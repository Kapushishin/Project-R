using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter(PlayerUnit player);
    public void HandleInput(PlayerUnit player);
    public void LogicUpdate(PlayerUnit player);
    public void PhysicsUpdate(PlayerUnit player);
    public void Exit(PlayerUnit player);
}
