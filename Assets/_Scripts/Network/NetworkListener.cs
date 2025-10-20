using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Text;

namespace Ksy.Network
{
    public class NetworkListener : MonoBehaviour
    {
        private Client _client;

        private Socket _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private IPEndPoint _iP = new IPEndPoint(IPAddress.Any, 7777);
        private OtherPlayerMovement _otherMovement;
        //private List<Socket> _connectClient = new List<Socket>();

        public void Init(Client client)
        {
            this._client = client;
            _otherMovement = _client.OtherPlayer.GetComponent<OtherPlayerMovement>();
        }
        public void StartSync()
        {
            Thread thread = new Thread(Listen);
            thread.Start();
            Debug.Log("Try Listen");
        }
        private void Listen()
        {
            _listener.Bind(_iP);
            _listener.Listen(5);

            while (true)
            {
                Socket connectClient = _listener.Accept();
                _client.networkSender.otherClient = connectClient;
                Debug.Log($"<color=green>Success Listen client</color>");
                //_connectClient.Add(connectClient);
                Thread listener = new Thread(()=> Receive(connectClient));
                listener.Start();
                return;
            }
        }

        public void Receive(Socket sender)
        {
            while (true)
            {
                Debug.Assert(sender != null, "<color=red>수신을 받으려 하는 클라이언트 소켓이 NULL입니다.");

                byte[] bytes = new byte[1024];
                int length = sender.Receive(bytes);
                try
                {
                    string strData = Encoding.UTF8.GetString(bytes, 0, length);
                    PlayerData otherPlayerData = JsonUtility.FromJson<PlayerData>(strData);

                    if (otherPlayerData != null)
                    {
                        if (_client.OtherPlayer != null)
                        {
                            //Debug.Log($"<color=green>Success Receive</color>");
                            _client.otherPlayerPos = otherPlayerData.Position;

                            //Vector2 moveDir = otherPlayerData.MoveDir - _otherMovement.MoveDir;
                            _otherMovement.MoveDir = otherPlayerData.MoveDir;

                            _client.IsLinearInterpolation_1 = true;
                        }
                    }
                }
                catch (SocketException e)
                {       
                    Debug.Log(e.ToString());
                    continue;
                }
                catch (System.Exception e)
                {
                    Debug.Log($"<color=red>Error : {e}</color>");
                }
            }
        }
    }
}

