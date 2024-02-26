using UnityEngine;

public class BlockState : IState
{
    private bool _blockInput;
    private bool _canBlock;
    private int _blockCounter;

    public void Enter(PlayerUnit player)
    {
        Debug.Log("Enter Block State");

        _blockCounter = player._blockTimer;
        player._canTakeDamage = false;
        _canBlock = true;

        _blockInput = player._input.BlockInput();
    }

    public void Exit(PlayerUnit player)
    {
        Debug.Log("Exit Block State");

        player._input.blockInput = false;
        player._canTakeDamage = true;
    }

    public void HandleInput(PlayerUnit player)
    {

    }

    public void LogicUpdate(PlayerUnit player)
    {
        if (!_canBlock)
        {
            player._stateMachine.ChangeState(player._basicState);
        }
    }

    public void PhysicsUpdate(PlayerUnit player)
    {
        if (_blockCounter > 0 && _canBlock)
        {
            _blockCounter--;
        }
        // еще если блок успешный (попадание в бит), то происходит контратака

        else
        {
            _canBlock = false;
        }
    }
}