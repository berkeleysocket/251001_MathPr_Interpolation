using Ksy.Network;
using UnityEngine;
using UnityEngine.InputSystem;

public class Client : MonoBehaviour
{
    public NetworkListener networkListener;
    public NetworkSender networkSender;

    private void Awake()
    {
        networkListener.GetComponent<NetworkListener>();
        networkSender.GetComponent<NetworkSender>();
    }

    private void Update()
    {
        if(Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            networkSender.StartSync(this);
        }
        else if(Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            networkListener.StartSync(this);
        }
    }
}