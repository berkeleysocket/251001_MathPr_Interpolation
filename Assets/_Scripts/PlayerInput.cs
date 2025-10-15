using Ksy.Entity.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerComponent
{
    public Vector2 MoveDir { get; private set; }
    public void OnMove(InputValue v)
    {
        MoveDir = v.Get<Vector2>();

        player.Client.networkSender.Send(Serialization(player.RbCompo.position));

        Debug.Log(MoveDir);
    }

    public PlayerData Serialization(Vector2 pos)
    {
        PlayerData myData = new PlayerData();

        myData.Position = pos;

        return myData;
    }
}
