using UnityEditor.SceneManagement;
using UnityEngine;

namespace Ksy.Entity.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerInput InputCompo { get; private set; }
        public PlayerMovement MovementCompo { get; private set; }
        public Rigidbody2D RbCompo { get; private set; }

        [field : SerializeField]
        public Client Client { get; private set; }


        private void Awake()
        {
            RbCompo = gameObject.AddComponent<Rigidbody2D>();
            RbCompo.gravityScale = 0;

            InputCompo = gameObject.AddComponent<PlayerInput>();
            InputCompo.Initialization(this);
            //InputCompo = gameObject.GetComponent<PlayerInput>();

            MovementCompo = gameObject.AddComponent<PlayerMovement>();
            MovementCompo.Initialization(this);
        }
    }

    public interface IPlayerComponent
    {
        public void Initialization(Player player);
        public Player reference();
    }

    public abstract class PlayerComponent : MonoBehaviour, IPlayerComponent
    {
        protected Player player;
        public void Initialization(Player player)
        {
           this.player = player;
        }
        public Player reference()
        {
            return player;
        }
    }
}
