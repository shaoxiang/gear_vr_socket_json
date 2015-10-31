using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ovr;
using SimpleJSON;
using System.Net.Sockets;
using System.IO;
using System;

public class Data_processing : MonoBehaviour {
	
	private Text TextSensorData;
	public double roll,yaw,pitch;
	public bool start_flag = false;
	//socket
	public String host = "localhost";
	public Int32 port = 50000;
	internal Boolean socket_ready = false;
	internal String input_buffer = "";
	TcpClient tcp_socket;
	NetworkStream net_stream;
	StreamWriter socket_writer;
	StreamReader socket_reader;

	void Awake()
	{
		setupSocket();
	}
	
	void Start () {
		TextSensorData = GetComponent<Text>();
		roll = 0;yaw = 0;pitch = 0;
	}
	
	void sensor_to_json(){
		var I = new JSONClass ();
		I["pitch"].AsDouble = pitch;
		I["yaw"].AsDouble = yaw;
		I["roll"].AsDouble = roll;
		writeSocket(I.ToString());
	}
	
	// Update is called once per frame
	//void Update(){

	//}
	void FixedUpdate() {
		pitch = OVRManager.display.GetHeadPose (0).orientation.eulerAngles.x;
		yaw = OVRManager.display.GetHeadPose (0).orientation.eulerAngles.y;
		roll = OVRManager.display.GetHeadPose (0).orientation.eulerAngles.z;
		//string received_data = readSocket();
		//if (Input.GetMouseButton (0)) {
			//start_flag = true;
		//} 
		//if (Input.GetKeyDown (KeyCode.Escape))
		//{
			//Application.Quit();
		//}
		//if (start_flag) {

			TextSensorData.text ="pitch:"+pitch.ToString()+"\n";//pitch
			TextSensorData.text +="yaw:"+yaw.ToString()+"\n";//yaw
			TextSensorData.text +="roll:"+roll.ToString()+"\n";//roll
			sensor_to_json ();
		//}
		
		//if (received_data != "")
		//{
			// Do something with the received data,
			// print it in the log for now
			//Debug.Log(received_data);
		//}
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

