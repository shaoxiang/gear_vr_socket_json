// PFC - prefrontal cortex
// Full Android Sensor Access for Unity3D
// Contact:
// 		contact.prefrontalcortex@gmail.com

using UnityEngine;
using System.Collections;

public class MinimalSensorCamera : MonoBehaviour {

	public string roll,yaw,pitch;
	private GUIStyle guiStyle = new GUIStyle(); //create a new variable
	// Use this for initialization
	void Start () {
		// you can use the API directly:
		// Sensor.Activate(Sensor.Type.RotationVector);
		
		// or you can use the SensorHelper, which has built-in fallback to less accurate but more common sensors:
		SensorHelper.ActivateRotation();
		
		useGUILayout = false;
	}
	
	// Update is called once per frame
	void Update () {
		// direct Sensor usage:
		// transform.rotation = Sensor.rotationQuaternion; --- is the same as Sensor.QuaternionFromRotationVector(Sensor.rotationVector);
		
		// Helper with fallback:
		transform.rotation = SensorHelper.rotation;
		roll = transform.rotation.eulerAngles.x.ToString ();
		yaw = transform.rotation.eulerAngles.y.ToString ();
		pitch = transform.rotation.eulerAngles.z.ToString ();
	}
	void OnGUI(){
		guiStyle.fontSize = 100;
		GUI.Label (new Rect (25, 25, 100, 30),roll,guiStyle);
		GUI.Label (new Rect (25, 155, 100, 30),yaw,guiStyle);
		GUI.Label (new Rect (25, 285, 100, 30),pitch,guiStyle);
	}
}