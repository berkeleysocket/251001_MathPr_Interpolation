using Ksy.Network;
using System.Collections;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Client : MonoBehaviour
{
    public NetworkListener networkListener;
    public NetworkSender networkSender;

    public static Thread MainTread;

    [field : SerializeField] public Rigidbody2D MyPlayer { get; private set; }
    [field : SerializeField] public Rigidbody2D OtherPlayer { get; private set; }

    public Vector2 otherPlayerPos = Vector2.zero;
    public Coroutine syncPos = null;
    public bool IsLinearInterpolation_1 = false;
    public float SyncPosSecondes_IsLinearInterpolation_1 = 0.01f;
    public float SyncPosSecondes = 0.001f;

    private void Awake()                                            
    {
        //메인스레드 파싱
        if (MainTread == null) MainTread = System.Threading.Thread.CurrentThread;

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

        //if(Keyboard.current.digit2Key.wasPressedThisFrame)
        //{
        //    IsLinearInterpolation = !IsLinearInterpolation;
        //}

        //if (!IsLinearInterpolation)
        //{
        //    OtherPlayer.transform.position = otherPlayerPos;
        //}
        //else if (syncPos == null)
        //{
        //    Debug.Log("IsLinearInterpolation");
        //    //syncPos = StartCoroutine(SyncPos());
        //}
        if (syncPos == null && IsLinearInterpolation_1)
        {
            syncPos = StartCoroutine(SyncPos());
        }
    }

    private IEnumerator SyncPos()
    {
        Vector2 last = OtherPlayer.transform.position;
        Vector2 current = otherPlayerPos;

        for (int g = 1; g <= 10; g++)
        {
            OtherPlayer.linearVelocity = Vector2.zero;
            OtherPlayer.transform.position = Lerp(last, current, (float)g / 10f);
            if(IsLinearInterpolation_1)
            {
                yield return new WaitForSeconds(SyncPosSecondes_IsLinearInterpolation_1);
            }
            else
            {
                yield return new WaitForSeconds(SyncPosSecondes);
            }
        }

        IsLinearInterpolation_1 = false;
        syncPos = null;
    }

    private Vector2 Lerp(Vector2 start, Vector2 end, float value)
    {
        float _value = Mathf.Clamp(value, 0, 1);

        float x_start = start.x;
        float y_start = start.y;

        float x_end = end.x;
        float y_end = end.y;

        //계산
        float lerpX = x_start + (x_end - x_start) * _value;
        float lerpY = y_start + (y_end - y_start) * _value;

        Vector2 result = new Vector2(lerpX, lerpY);

        return result;
    }

    //private void FixedUpdate()
    //{
    //    //OtherPlayer.linearVelocity = (otherPlayerPos - OtherPlayer.position) * 1.5f;
    //}
}