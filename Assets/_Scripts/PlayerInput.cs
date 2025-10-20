using Ksy.Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerComponent
{
    public Vector2 MoveDir { get; private set; }
    public void OnMove(InputValue v)
    {
        MoveDir = v.Get<Vector2>();
        if (player.Client.networkSender != null)
        {
            player.Client.networkSender.Send(player.Serialization(transform.position, MoveDir));
        }
    }
        //currentTime += Time.deltaTime;
        //if (currentTime >= sendRate)
        //{
        //    currentTime = 0f;
        //    if (Client.networkSender != null)
        //    {
        //        Client.networkSender.Send(Serialization(transform.position));
        //    }
        //}
}
