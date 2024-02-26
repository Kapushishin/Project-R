using UnityEngine;

public class HugeAttackState : IState
{
    private bool _hugeAttackInput;
    private Vector3 _mouseInput;
    private bool _canAttack;
    private int _attackCDCounter;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Huge Attack State");

        _attackCDCounter = player._attackCD;
        _canAttack = true;
    }

    public void Exit(PlayerUnit player)
    {
        player._input.hugeAttackInput = false;
        Debug.Log("Exit Huge Attack State");
    }

    public void HandleInput(PlayerUnit player)
    {
        _hugeAttackInput = player._input.HugeAttackInput();
    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (!_canAttack)
        {
            player._stateMachine.ChangeState(player._basicState);
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        if (_attackCDCounter > 0 && _canAttack)
        {
            _attackCDCounter--;
            //attack
        }
        else
        {
            _canAttack = false;
        }
    }
}
