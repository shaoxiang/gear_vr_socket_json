using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Ovr;
using SimpleJSON;
using System.Net;  
using System.Net.Sockets;
using System.IO;
using System;
using System.Text; 

public class Data_processing : MonoBehaviour {
	
	private Text TextSensorData;
	public double roll,yaw,pitch;
	public bool start_flag = false;
	//socket
	IPAddress ipAdr ;  
	IPEndPoint ipEp ; 
	Socket clientScoket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
	internal Boolean socket_ready = false;

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
		///Touch Pad
		///Input.mousePosition.x;Input.mousePosition.y;Input.GetMouseButtonDown (0);Input.GetMouseButtonUp (0);
		//if (Input.GetMouseButton (0)) {
			//start_flag = true;
		//}
		//if (start_flag) {
		//click the touch button to start.Do something.
		//}
		if (Input.GetKeyDown (KeyCode.Escape)	)
		{
			Application.Quit();
		}
		TextSensorData.text ="pitch:"+pitch.ToString()+"\n";//pitch
		TextSensorData.text +="yaw:"+yaw.ToString()+"\n";//yaw
		TextSensorData.text +="roll:"+roll.ToString()+"\n";//roll
		sensor_to_json ();
		//read data
		readSocket ();
	}
			
	void OnApplicationQuit()
	{
		closeSocket();
	}
	
	public void setupSocket()
	{
		try
		{
			socket_ready = true;
			ipAdr = IPAddress.Parse("127.0.0.1");//localhost
			ipEp = new IPEndPoint (ipAdr, 50000);
			clientScoket.Connect(ipEp);
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
		byte[] concent = Encoding.UTF8.GetBytes(line);          
		clientScoket.Send(concent);
	}
	
	public void readSocket()
	{
		if (!socket_ready)
			return;
		byte[] response = new byte[1024];
		int bytesRead = clientScoket.Receive(response);  
		string input = Encoding.UTF8.GetString(response,0,bytesRead);
		Debug.Log("Client request:"+input); 
	}
	
	public void closeSocket()
	{
		if (!socket_ready)
			return;
		clientScoket.Shutdown(SocketShutdown.Both);  
		clientScoket.Close(); 
		socket_ready = false;
	}
}

