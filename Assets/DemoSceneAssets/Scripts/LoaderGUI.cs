// #######################################
// ---------------------------------------
// ---------------------------------------
// PFC - prefrontal cortex
// ---------------------------------------
// Full Android Sensor Access for Unity3D
// ---------------------------------------
// Contact:
// 		contact.prefrontalcortex@gmail.com
// ---------------------------------------
// #######################################


using UnityEngine;
using System.Collections;
using System;

public static class GUITools {
	public static bool Enabled = false;
	
	public static float DpiScaling {
		get {
			if(Enabled)
				return Screen.dpi / 160.0f;
			else
				return 160.0f / 160.0f;
		}
	}
}

public class LoaderGUI : MonoBehaviour {

	[Serializable]
	public class SceneData {
		public string name;
		public string path;
	}
	
	void Awake() {
		GUITools.Enabled = true;
	}
	
	void Disable() {
		GUITools.Enabled = false;
	}
	
	public SceneData[] scenes;
	public GUISkin guiSkin;

	Vector2 scrollVector = Vector2.zero;
	void OnGUI() {
		float dpiScaling = GUITools.DpiScaling;

		GUI.color = Color.white;
		GUI.skin = guiSkin;
		
		// Screen.height - Screen.height / 5
		scrollVector = GUI.BeginScrollView(new Rect(Screen.width/2 - 100 * dpiScaling, Screen.height/10, 230 * dpiScaling, Screen.height * 0.8f), scrollVector, new Rect(0,0,200 * dpiScaling, (scenes.Length * 50 + 50) * dpiScaling));
		
//		GUILayout.BeginArea(new Rect(0, 0, 300, scenes.Length * 50));
		
		foreach(SceneData s in scenes)
		if(GUILayout.Button(s.name, GUILayout.Height(50 * dpiScaling), GUILayout.Width(230 * dpiScaling)))
		{
			Debug.Log("Loading scene: " + s.name);
			Application.LoadLevel(s.path);
		}
		
//		GUILayout.EndArea();
		
		GUI.EndScrollView();
	}
}
