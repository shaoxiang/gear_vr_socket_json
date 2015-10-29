using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {
	
	public Camera groundCamera;
	public Camera airCamera;
	
	Rect r = new Rect(10,250,100 * GUITools.DpiScaling,50 * GUITools.DpiScaling);
	void OnGUI() {
		if(GUI.Button(r, "Switch Camera"))
			SwitchCam();
	}
	
	void SwitchCam() {
#pragma warning disable 0618
		groundCamera.gameObject.active = !groundCamera.gameObject.active;
		airCamera.gameObject.active = !groundCamera.gameObject.active;
#pragma warning restore
	}
}
