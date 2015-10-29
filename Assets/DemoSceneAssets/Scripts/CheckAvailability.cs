// PFC - prefrontal cortex
// Full Android Sensor Access for Unity3D
// Contact:
// 		contact.prefrontalcortex@gmail.com

using UnityEngine;
using System.Collections;

public class CheckAvailability : MonoBehaviour {
	
	public Color guiColor = Color.white;
	public string sceneDescription = "";
	
	public string CheckForErrors() {
		string goodOutput = "";
		string badOutput = "";
		
		for(int i = 1; i <= Sensor.Count; i++)
		{
			Sensor.Information info = Sensor.Get((Sensor.Type)i);
			if(info == null)
				continue;
			goodOutput += info.active && info.available ? "\n\t" + info.description  : "";
			badOutput  += info.active && !info.available ? "\n\t" + info.description : "";	
		}
		
		return "From the sensors needed for this scene, your device: \n" +
			"supports: "     + goodOutput + "\n"  + 
			"not supports: " + badOutput;
	}
	
	string message;
		
	void Start() {
		message = "";	
	}
	
	void OnGUI() {
		if(message == "")
			message = CheckForErrors();
		
		GUI.color = guiColor;
		
		GUILayout.BeginArea(new Rect(10,10,Screen.width-20, 200));
		if(sceneDescription != "") GUILayout.Label(sceneDescription);
		GUILayout.Label(message);
		GUILayout.EndArea();
	}
}
