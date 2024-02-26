using UnityEngine;

public class BasicState : IState
{
    private Vector2 _movementInput;
    private Vector3 _lookInput;

    private bool _jumpInput;
    private bool _dashInput;
    private bool _blockInput;
    private bool _attackInput;
    private bool _hugeAttackInput;

    private bool _canAct;
    private int _globalCDCounter;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Basic State");

        _canAct = false;
        _globalCDCounter = player._globalCD;
    }

    public void Exit(PlayerUnit player)
    {
        Debug.Log("Exit Basic State");
    }

    public void HandleInput(PlayerUnit player)
    {
        _lookInput = player._input.LookInput();
        _movementInput = player._input.MovementInput();
        _jumpInput = player._input.JumpInput();
        _dashInput = player._input.DashInput();
        _blockInput = player._input.BlockInput();
        _attackInput = player._input.AttackInput();
        _hugeAttackInput = player._input.HugeAttackInput();
    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (_canAct)
        {
            if (_dashInput)
            {
                player._stateMachine.ChangeState(player._dashState);
            }
            else if (_jumpInput)
            {
                player._stateMachine.ChangeState(player._jumpState);
            }
            else if (_blockInput)
            {
                player._stateMachine.ChangeState(player._blockState);
            }
            else if (_attackInput)
            {
                player._stateMachine.ChangeState(player._attackState);
            }
            else if (_hugeAttackInput)
            {
                player._stateMachine.ChangeState(player._hugeAttackState);
            }
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        if (_globalCDCounter >0 && !_canAct)
        {
            _globalCDCounter--;
        }
        else
        {
            _canAct = true;
        }
        player._inputController.GroundCheck(player);
        player._inputController.Gravity(player);
        player._inputController.Move(player, _movementInput);
        player._inputController.CameraRotation(player, _lookInput);

    }
}
