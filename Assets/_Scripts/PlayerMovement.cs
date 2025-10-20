using UnityEngine;

namespace Ksy.Entity.Player
{
    public class PlayerMovement : PlayerComponent
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

        public bool IsMine;

        public Vector2 MoveDir = Vector2.zero;

        private void Update()
        {
            if (player.InputCompo != null)
            {
                MoveDir = player.InputCompo.MoveDir;
            }
        }
        private void FixedUpdate()
        {
            player.RbCompo.linearVelocity = MoveDir * MoveSpeed;
        }
    }
}


