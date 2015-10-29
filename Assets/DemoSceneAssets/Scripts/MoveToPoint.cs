using UnityEngine;
using System.Collections;

public class MoveToPoint : MonoBehaviour {
	
	Vector3 lastPoint;
	RaycastHit hit;
	
	Touch t;
	Touch lastTouch;
	
	bool haveLastTouch = false;
	
	// Update is called once per frame
	void Update () {
		if(haveLastTouch) {
			if(Physics.Raycast(Camera.main.ScreenPointToRay(lastTouch.position), out hit, Mathf.Infinity)) {
				lastPoint = hit.point;
			}
		}
		
		if(haveLastTouch = (Input.touchCount == 1)) {
			t = Input.GetTouch(0);
			
			if(Physics.Raycast(Camera.main.ScreenPointToRay(t.position), out hit, Mathf.Infinity)) {
				if(t.phase != TouchPhase.Began && t.phase != TouchPhase.Ended) {
					// move by distance between last and current point
					transform.position += lastPoint - hit.point;
				}
				
				lastPoint = hit.point;
			}
			
			lastTouch = t;
		}
	}
}
