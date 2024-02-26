using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class BasicInputController : IInputController
{
    public void GroundCheck(PlayerUnit player)
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(player.transform.position.x, player.transform.position.y - player._groundedOffset,
            player.transform.position.z);
        player._grounded = Physics.CheckSphere(spherePosition, player._groundedRadius, player._groundLayers,
            QueryTriggerInteraction.Ignore);
        /*
        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, Grounded);
        }
        */
    }

    public void Gravity(PlayerUnit player)
    {
        if (player._grounded)
        {
            // reset the fall timeout timer
            player._fallTimeoutDelta = player._fallTimeout;

            /*
            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }
            */

            // stop our velocity dropping infinitely when grounded
            if (player._verticalVelocity < 0.0f)
            {
                player._verticalVelocity = -2f;
            }
        }
            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (player._verticalVelocity < player._terminalVelocity)
        {
            player._verticalVelocity += player._gravity * Time.deltaTime;
        }
    }

    public void Jump(PlayerUnit player, bool jumpInput)
    {
        if (player._grounded)
        {
            // Jump
            if (jumpInput && player._jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                player._verticalVelocity = Mathf.Sqrt(player._jumpHeight * -2f * player._gravity);

                /*
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, true);
                }
                */
            }

            // jump timeout
            if (player._jumpTimeoutDelta >= 0.0f)
            {
                player._jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            player._jumpTimeoutDelta = player._jumpTimeout;

            // fall timeout
            if (player._fallTimeoutDelta >= 0.0f)
            {
                player._fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                /*
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
                */
            }

            // if we are not grounded, do not jump
            jumpInput = false;
        }
    }

    public void CameraRotation(PlayerUnit player, Vector2 lookInput)
    {
        // if there is an input and camera position is not fixed
        if (lookInput.sqrMagnitude >= player._threshold && !player._lockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            //float deltaTimeMultiplier = player._isCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            player._cinemachineTargetYaw += lookInput.x * deltaTimeMultiplier;
            player._cinemachineTargetPitch += lookInput.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        player._cinemachineTargetYaw = ClampAngle(player._cinemachineTargetYaw, float.MinValue, float.MaxValue);
        player._cinemachineTargetPitch = ClampAngle(player._cinemachineTargetPitch, player._bottomClamp, player._topClamp);

        // Cinemachine will follow this target
        player._cinemachineCameraTarget.transform.rotation = Quaternion.Euler(player._cinemachineTargetPitch + player._cameraAngleOverride,
            player._cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void Move(PlayerUnit player, Vector2 movementInput)
    {
        float speed;
        // set target speed based on move speed, sprint speed and if sprint is pressed
        //float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
        float targetSpeed = player._movementSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (movementInput == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(player._characterController.velocity.x, 0.0f, player._characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = movementInput.magnitude;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * player._speedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        player._animationBlend = Mathf.Lerp(player._animationBlend, targetSpeed, Time.deltaTime * player._speedChangeRate);
        if (player._animationBlend < 0.01f) player._animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (movementInput != Vector2.zero)
        {
            player._targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              player._mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, player._targetRotation, ref player._rotationVelocity,
                player._rotationSmoothTime);

            // rotate to face input direction relative to camera position
            player.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, player._targetRotation, 0.0f) * Vector3.forward;

        // move the player
        player._characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                         new Vector3(0.0f, player._verticalVelocity, 0.0f) * Time.deltaTime);
        /*
        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
        */
    }

    public void Dash(PlayerUnit player)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(PlayerUnit player)
    {
        throw new System.NotImplementedException();
    }

    public void HugeAttack(PlayerUnit player)
    {
        throw new System.NotImplementedException();
    }

    public void Block(PlayerUnit player)
    {
        throw new System.NotImplementedException();
    }
}
