using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using SimpleJSON;
 

public class networkSocket : MonoBehaviour
{
	public int roll=0,yaw=0,pitch=0;
	public int yaw_start ,yaw_now;
	public int i=0;
	public bool start_flag=false;
	private GUIStyle guiStyle = new GUIStyle(); //create a new variable

	public String host = "localhost";
    public Int32 port = 50000;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;
	//public string txt;

	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;


	void P(string aText)
	{
		m_InGameLog = aText;
	}
	

	void Start(){
		//txt = "700";
		// Sensor.Activate(Sensor.Type.RotationVector);
		// or you can use the SensorHelper, which has built-in fallback to less accurate but more common sensors:
		SensorHelper.ActivateRotation();
		transform.rotation = SensorHelper.rotation;
		//useGUILayout = false;
		Debug.Log("Test results:\n" + m_InGameLog);
		yaw_start = 0;
	}

	void test(){
		var I = new JSONClass ();
		I["pitch"].AsInt = pitch;
		I["yaw"].AsInt = yaw;
		writeSocket(I.ToString());

		P(I.ToString());

	}

	void FixedUpdate()
    {
		string received_data = readSocket();
		transform.rotation = SensorHelper.rotation;
		//roll = (int)transform.rotation.eulerAngles.x;
		//yaw = (int)transform.rotation.eulerAngles.y;
		//pitch = (int)transform.rotation.eulerAngles.z; 
		i++;
		if (i > 10000) {
			i=0;
		}
		if(i%5==0){
			yaw_start=(int)transform.rotation.eulerAngles.y;
		}
		if (start_flag) {

			yaw_now = (int)transform.rotation.eulerAngles.y;
			yaw = yaw_now - yaw_start;
			if(yaw<-180){yaw=yaw+360;}
			else if(yaw>180){yaw=yaw-360;}
			pitch = (int)transform.rotation.eulerAngles.x;
			if (pitch >= 180 && pitch <= 360) {
				pitch = pitch - 360;
			}
			test ();
		} else {
			if(!start_flag){
				yaw_start=(int)transform.rotation.eulerAngles.y;
			}
		}

        if (received_data != "")
        {
        	// Do something with the received data,
        	// print it in the log for now
            Debug.Log(received_data);
        }

    }

	//void OnGUI(){
		//txt = GUI.TextField (new Rect(80,20,80,100),txt);
	//}

	void OnGUI(){
		guiStyle.fontSize = 100;
		GUI.Label (new Rect (25, 25, 100, 30),roll.ToString(),guiStyle);
		GUI.Label (new Rect (25, 155, 100, 30),yaw.ToString(),guiStyle);
		GUI.Label (new Rect (25, 285, 100, 30),pitch.ToString(),guiStyle);
		GUI.Label (new Rect (25, 385, 100, 30),yaw_start.ToString(),guiStyle);
		GUI.Label (new Rect (25, 485, 100, 30),yaw_now.ToString(),guiStyle);
		GUI.Label (new Rect (25, 585, 100, 30),m_InGameLog,guiStyle);
		if (GUI.Button (new Rect (500, 350, 200, 200), "start",guiStyle)) {
			start_flag=true;
		}
		if (GUI.Button (new Rect (1000, 350, 200, 200), "stop",guiStyle)) {
			start_flag=false;
		}
	}

    void Awake()
    {
        setupSocket();
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;
            
        //line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!socket_ready)
            return "";

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

}