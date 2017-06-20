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
		if(col.transform.name == "SMGPickup"){
			transform.BroadcastMessage("Pickup", 4);
			Destroy(col.transform.gameObject);
		}
        if(col.transform.name == "PistolAmmo"){
            transform.BroadcastMessage("MoreAmmoPlease", 1);
            Destroy(col.transform.gameObject);
        }
        if (col.transform.name == "ShotgunAmmo")
        {
            transform.BroadcastMessage("MoreAmmoPlease", 3);
            Destroy(col.transform.gameObject);
        }
        if (col.transform.name == "SMGAmmo")
        {
            transform.BroadcastMessage("MoreAmmoPlease", 5);
            Destroy(col.transform.gameObject);
        }
        if (col.transform.name == "HealthPack")
        {
            transform.BroadcastMessage("WeNeedHealing", 6);
            Destroy(col.transform.gameObject);
        }
        if (col.transform.name == "ArmorPack")
        {
            transform.BroadcastMessage("WeNeedHealing", 7);
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
		7: Armor
	*/
}
