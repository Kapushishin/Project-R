using UnityEngine;

public interface IInputController
{
    void Move(PlayerUnit player, Vector2 movementInput);
    void CameraRotation(PlayerUnit player, Vector2 lookInput);
    void Jump(PlayerUnit player, bool jumpInput);
    void GroundCheck(PlayerUnit player);
    void Gravity(PlayerUnit player);
    void Dash(PlayerUnit player);
    void Block(PlayerUnit player);
    void Attack(PlayerUnit player);
    void HugeAttack(PlayerUnit player);

}