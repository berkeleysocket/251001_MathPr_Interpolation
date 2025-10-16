using Ksy.Entity.Player;
using Ksy.Network;
using UnityEngine;
using UnityEngine.InputSystem;

public class Client : MonoBehaviour
{
    public NetworkListener networkListener;
    public NetworkSender networkSender;

    [field : SerializeField] public Rigidbody2D MyPlayer { get; private set; }
    [field : SerializeField] public Rigidbody2D OtherPlayer { get; private set; }

    public Vector2 otherPlayerPos = Vector2.zero;

    private void Awake()
    {
        networkListener = GetComponent<NetworkListener>();
        networkSender = GetComponent<NetworkSender>();

        networkListener.Init(this);
        networkSender.Init(this);
    }

    private void Update()
    {
        if(Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            networkSender.StartSync();
        }
        else if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            networkListener.StartSync();
        }
    }

    private void FixedUpdate()
    {
        OtherPlayer.linearVelocity = (otherPlayerPos - OtherPlayer.position) * 1.5f;
    }
}