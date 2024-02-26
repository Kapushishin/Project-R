using UnityEngine;

public interface IInput
{
    Vector2 MovementInput();
    Vector3 LookInput();
    bool JumpInput();
    bool DashInput();
    bool BlockInput();
    bool AttackInput();
    bool HugeAttackInput();
}
