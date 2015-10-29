using UnityEngine;
using System.Collections;

public class CloseApplication : MonoBehaviour {
	
	KeyCode key = KeyCode.Escape;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(key))
			Application.Quit();
	}
}
