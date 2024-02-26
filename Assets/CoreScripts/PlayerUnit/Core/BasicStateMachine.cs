using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateMachine
{
    public IState CurrentState;
    private PlayerUnit _player;

    public void Initialize(PlayerUnit player, IState startingState)
    {
        CurrentState = startingState;
        _player = player;
        startingState.Enter(_player);
    }

    public void ChangeState(IState newState)
    {
        CurrentState.Exit(_player);

        CurrentState = newState;
        newState.Enter(_player);
    }
}
