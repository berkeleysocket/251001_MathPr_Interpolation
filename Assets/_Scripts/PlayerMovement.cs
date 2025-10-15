using UnityEngine;

namespace Ksy.Entity.Player
{
    public class PlayerMovement : PlayerComponent
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

        private Vector2 moveDir => player.InputCompo.MoveDir;

        private void FixedUpdate()
        {
            player.RbCompo.linearVelocity = moveDir * MoveSpeed;
        }
    }
}


