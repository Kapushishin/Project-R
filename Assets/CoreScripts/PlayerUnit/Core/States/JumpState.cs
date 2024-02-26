using UnityEngine;

public class JumpState : IState
{
    private bool _jumpInput;
    private Vector3 _lookInput;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Jump State");
    }

    public void Exit(PlayerUnit player)
    {
        _jumpInput = false;
        Debug.Log("Exit Jump State");
    }

    public void HandleInput(PlayerUnit player)
    {
        _jumpInput = player._input.JumpInput();
        _lookInput = player._input.LookInput();
    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (!player._input.jumpInput)
        {
            player._stateMachine.ChangeState(player._basicState);
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        player._inputController.GroundCheck(player);
        player._inputController.Gravity(player);
        player._inputController.CameraRotation(player, _lookInput);
        player._inputController.Jump(player, _jumpInput);
    }
}
