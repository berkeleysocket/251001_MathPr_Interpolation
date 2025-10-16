using Ksy.Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerComponent
{
    public Vector2 MoveDir { get; private set; }
    public void OnMove(InputValue v)
    {
        MoveDir = v.Get<Vector2>();
    }
}
