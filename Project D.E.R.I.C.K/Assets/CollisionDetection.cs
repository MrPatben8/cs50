using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.name == "ShotgunPickup"){
			transform.BroadcastMessage("Pickup", 2);
			Destroy(col.transform.gameObject);
		}
	}
	/*
		1: Pistol Ammo
		2: Shotgun
		3: Shotgun Ammo
		4: SMG
		5: SMG Ammo
		6: Medkit
	*/
}
