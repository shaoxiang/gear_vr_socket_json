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

public class DampenCameraMovement : MonoBehaviour {
	
	public Transform target;
	public Transform lookAt;
	public float movementSpeed = 3;
	public float rotationSpeed = 3;
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * movementSpeed);
		transform.LookAt(lookAt.position, target.up);
	}
}
