using UnityEngine;

public class DashState : IState
{
    private Vector2 _movementInput;
    private Vector2 _mouseInput;
    private bool _dashInput;
    private bool _canDash;
    private int _dashCounter;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Dash State");

        _dashCounter = player._dashTimer;
        player._canTakeDamage = false;
        _canDash = true;

        _movementInput = player._input.MovementInput();
        _dashInput = player._input.DashInput();
        _mouseInput = player._input.LookInput();
    }

    public void Exit(PlayerUnit player)
    {
        Debug.Log("Exit Dash State");
        player._input.dashInput = false;
        player._canTakeDamage = true;
    }

    public void HandleInput(PlayerUnit player)
    {
        // All inputs traveled to Enter method :c
    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (!_canDash)
        {
            player._stateMachine.ChangeState(player._basicState);
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        if (_dashCounter > 0 && _canDash)
        {
            _dashCounter--;
            DashUpdate(player);
        }
        else
        {
            _canDash = false;
        }
    }

    private void DashUpdate(PlayerUnit player)
    {
        Vector2 currentPosition = player._rigidbody.position;
        Vector2 inputVector = _movementInput;

        if (Mathf.Abs(inputVector.x) > 0 || Mathf.Abs(inputVector.y) > 0)
        {
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Debug.Log("Input Vector: " + inputVector);
            Vector2 movement = inputVector * player._dashSpeed;
            Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;
            player._rigidbody.MovePosition(newPosition);
        }
        else
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(_mouseInput) - player.transform.position;
            direction = Vector2.ClampMagnitude(direction, 1);
            Debug.Log("Direction " + direction);
            Vector2 movement = direction * player._dashSpeed;
            Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;
            player._rigidbody.MovePosition(newPosition);
        }
    }
}
