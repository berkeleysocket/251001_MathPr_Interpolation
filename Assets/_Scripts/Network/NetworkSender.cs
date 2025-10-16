using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Ksy.Network
{
    public class NetworkSender : MonoBehaviour
    {
        private Client _client;

        private Socket _sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Socket otherClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void Init(Client client)
        {
            this._client = client;
        }
        public void StartSync()
        {
            Thread thread = new Thread(Connect);
            thread.Start();
            Debug.Log("Try Connecting");
        }
        public void Connect()
        {
            IPAddress adress = new IPAddress(new byte[] { 127,0,0,1 });
            IPEndPoint endPoint = new IPEndPoint(adress, 7777);

            //AddressFamily addressT = AddressFamily.InterNetwork;
            //SocketType socketT = SocketType.Stream;
            //ProtocolType protocolT = ProtocolType.Tcp;

            //블로킹 함수임
            otherClient.Connect(endPoint);
            Debug.Log($"<color=green>Success Find client</color>");

            //수신 스레드 생성
            //Thread thread_send = new Thread(() => SendMessage(server));
            Thread receiver = new Thread(() => _client.networkListener.Receive(otherClient));

            //스레드 시작
            receiver.Start();
        }

        public void Send(PlayerData myData)
        {
            if (otherClient == null) return;
            byte[] bff = new byte[1024];

            try
            {
                string _myData = JsonUtility.ToJson(myData);
                int length = Encoding.UTF8.GetBytes(_myData, 0, _myData.Length, bff, 0);
                otherClient.Send(bff, length, SocketFlags.None);
            }
            catch (SocketException e)
            {
                Debug.Log(e.ToString());
                return;
            }
            catch
            {
                Debug.Log("Unknown Error");
                return;
            }

            Debug.Log($"<color=green>Success Send</color>");
        }
    }
}
