using UnityEngine;
using System.Collections;

public class CreateHouses : MonoBehaviour {
	
	public Transform housePrefab;
	public float worldSize = 20;
	public int count = 20;
	
	// Use this for initialization
	void Start () {
		
		Vector3 randomPosition;
		
		// place some houses...
		for(int i = 0; i < count; i++) {
			randomPosition = new Vector3((Random.value - 0.5f) * worldSize, 0, (Random.value - 0.5f) * worldSize);
			Transform newHouse = (Transform) Instantiate(housePrefab, randomPosition, Quaternion.Euler(0,Random.value * 360, 0));
			newHouse.localScale = new Vector3(Random.value * 2 + 1,Random.value * 2 + 0.2f,Random.value * 2 + 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
