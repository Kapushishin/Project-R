using UnityEngine;
using UnityEngine.InputSystem;

public class BasicInput : MonoBehaviour, IInput
{
    public Vector3 lookInput;
    public Vector2 moveInput;
    public bool jumpInput;
    public bool dashInput;
    public bool blockInput;
    public bool attackInput;
    public bool hugeAttackInput;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public Vector3 LookInput()
    {
        return lookInput;
    }

    public Vector2 MovementInput()
    {
        return moveInput;
    }

    public bool JumpInput()
    {
        Debug.Log(jumpInput);
        return jumpInput;
    }

    public bool DashInput()
    {
        return dashInput;
    }

    public bool BlockInput()
    {
        return blockInput;
    }

    public bool AttackInput()
    {
        return attackInput;
    }

    public bool HugeAttackInput()
    {
        return hugeAttackInput;
    }

    private void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            lookInput = value.Get<Vector2>();
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 movementInput = value.Get<Vector2>();
        moveInput = movementInput;
    }

    private void OnJump(InputValue value)
    {
        jumpInput = value.isPressed;
    }

    private void OnDash(InputValue value)
    {
        dashInput = value.Get<float>() > 0f;
    }

    private void OnBlock(InputValue value)
    {
        blockInput = value.Get<float>() > 0f;
    }

    private void OnAttack(InputValue value)
    {
        attackInput = value.isPressed;
    }

    private void OnHugeAttack(InputValue value)
    {
        hugeAttackInput = value.isPressed;
    }
}
