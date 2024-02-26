using UnityEngine;

public class AttackState : IState
{
    private bool _attackInput;
    private Vector3 _mouseInput;
    private bool _canAttack;
    private int _attackCDCounter;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Attack State");

        _attackCDCounter = player._attackCD;
        _canAttack = true;
    }

    public void Exit(PlayerUnit player)
    {
        player._input.attackInput = false;
        Debug.Log("Exit Attack State");
    }

    public void HandleInput(PlayerUnit player)
    {
        _attackInput = player._input.AttackInput();
    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (_attackCDCounter <= 0)
        {
            player._stateMachine.ChangeState(player._basicState);
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        if (_canAttack)
        {
            //attack
        }

        if (_attackCDCounter > 0)
        {
            _attackCDCounter--;
            Debug.Log(_attackCDCounter);
            _canAttack = false;
        }
    }
}
