using Ksy.Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerComponent
{
    public Vector2 MoveDir { get; private set; }
    public bool toggle;
    public float currentTime;
    public float sendRate = 0.01f;
    public void OnMove(InputValue v)
    {
        MoveDir = v.Get<Vector2>();

        if (player.Client.networkSender != null && toggle)
        {
            Debug.Log("<color=blue>SendDead</color>");
            player.Client.networkSender.Send(player.Serialization(transform.position, MoveDir));
        }
    }

    private void Update()
    {
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            toggle = !toggle;
        }
        if(!toggle)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= sendRate)
            {
                currentTime = 0f;
                if (player.Client.networkSender != null)
                {
                    Debug.Log("<color=green>SendDead</color>");
                    player.Client.networkSender.Send(player.Serialization(transform.position, Vector2.zero));
                }
            }
        }
    }

}
