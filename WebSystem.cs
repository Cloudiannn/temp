using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class WebSystem : MonoBehaviour
{
    
    public int port = 9000;
    static Socket server;
    private UdpClient client; // 客户端
    private IPEndPoint remotePoint;
    private Thread thread = null;
    private bool msgReceived;
    public enum actType{None, Connect, Disconnect, Click, Space};
    private string receiveString = null;
    private MsgData msg;

    class MsgData
    {
        public actType act;
        public int?[] clickPos;
        //public 
        public string PackUpToString(MsgData msg)
        {
            return (string)(act.ToString() + "#" + clickPos[0].ToString() + "#" + clickPos[1].ToString());
        }
        public void Unpack(string msgString, ref MsgData msg)
        {
            string[] msgDataStr = msgString.Split(' ');
            msg.act = (actType)System.Enum.Parse(typeof(actType), msgDataStr[0]);
            if(msg.act == actType.Click)
            {
                msg.clickPos[0] = int.Parse(msgDataStr[1]);
                msg.clickPos[1] = int.Parse(msgDataStr[2]);
            }
        }
        public MsgData()
        {
            act = actType.None;
            clickPos[0] = null;
            clickPos[1] = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        remotePoint = new IPEndPoint(IPAddress.Loopback, port);
        thread = new Thread(ReceiveData);
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(receiveString))
        {
            msg.Unpack(receiveString,ref msg);

            receiveString = null;
        }
        
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            if (string.IsNullOrEmpty(receiveString))
            {
                try
                {
                    //  Setup UDP client.
                    client = new UdpClient(port);

                    //  Grab the data.
                    byte[] data = client.Receive(ref remotePoint);
                    receiveString = Encoding.UTF8.GetString(data);
                    client.Close();

                }
                catch (System.Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
    }

    public void SendMessage(string remoteIP, int remotePort, string message)
    {
        
        byte[] sendbytes = Encoding.Unicode.GetBytes(message);
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();

        //发送数据到对应目标
        udpSend.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
        //关闭
        udpSend.Close();
    }
    

    void OnApplicationQuit() 
    {
        SocketQuit();
    }

    void OnDestroy()
    {
        SocketQuit();
    }

    // 终止线程，关闭client
    void SocketQuit() 
    {
        thread.Abort();
        thread.Interrupt();
        client.Close();
    }
}
