using UnityEngine;

public class OtherPlayerMovement : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

    public Vector2 MoveDir = Vector2.zero;
    public Rigidbody2D RbCompo;
    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        RbCompo.linearVelocity = MoveDir * MoveSpeed;
    }
}
