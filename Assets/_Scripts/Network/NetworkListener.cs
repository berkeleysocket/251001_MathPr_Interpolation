using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace Ksy.Network
{
    public class NetworkListener : MonoBehaviour
    {
        [SerializeField] private GameObject otherPlayer;

        private Client _client;

        private Socket _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private IPEndPoint _iP = new IPEndPoint(IPAddress.Any, 7777);
        //private List<Socket> _connectClient = new List<Socket>();

        public void StartSync(Client client)
        {
            this._client = client;
            Thread thread = new Thread(Listen);
            thread.Start();
        }
        private void Listen()
        {
            _listener.Bind(_iP);
            _listener.Listen(5);

            while (true)
            {
                Socket connectClient = _listener.Accept();
                //_connectClient.Add(connectClient);
                Thread listener = new Thread(()=>Listen(connectClient));
                listener.Start();
                return;
            }
        }

        public void Listen(Socket sender)
        {
            while(true)
            {
                byte[] bytes = new byte[1024];
                int length = sender.Receive(bytes);
                try
                {
                    string strData = Encoding.UTF8.GetString(bytes, 0, length);
                    PlayerData otherPlayerData = JsonUtility.FromJson<PlayerData>(strData);

                    if (otherPlayerData != null)
                    {
                        if (otherPlayer != null)
                        {
                            otherPlayer.transform.position = otherPlayerData.Position;
                        }
                    }
                }
                catch (SocketException e)
                {
                    Debug.Log(e.ToString());
                    continue;
                }
                catch
                {
                    Debug.Log("Unknown Error");
                }
            }
        }
    }
}

