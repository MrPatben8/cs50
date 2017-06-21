using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CollisionDetection : MonoBehaviour {

	// Use this for initialization
	public Rigidbody rig;
	public FirstPersonController fir;
	public CharacterController cha;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col){
		/*if (col.transform.tag == "Ladder") {
			if (Input.GetKey (KeyCode.W)) {
				fir.enabled = false;
				rig.isKinematic = true;
				cha.enabled = false;
				Vector3 pos = transform.position;
				pos.y += 0.1f;
				transform.position = pos;
				Debug.Log ("GOING UP");
			}
			//rig.isKinematic = false;
			//fir.enabled = true;
			//cha.enabled = true;
		}*/
	}

	void OnTriggerExit(Collider col){
		if (col.transform.tag == "Ladder") {

		}
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.name.Contains ("ShotgunPickup")){
			transform.BroadcastMessage("Pickup", 2);
			Destroy(col.transform.gameObject);
		}
		if(col.transform.name.Contains("SMGPickup")){
			transform.BroadcastMessage("Pickup", 4);
			Destroy(col.transform.gameObject);
		}
		if(col.transform.name.Contains("PistolAmmo")){
            transform.BroadcastMessage("MoreAmmoPlease", 1);
            Destroy(col.transform.gameObject);
        }
		if (col.transform.name.Contains("ShotgunAmmo")){
            transform.BroadcastMessage("MoreAmmoPlease", 3);
            Destroy(col.transform.gameObject);
        }
		if (col.transform.name.Contains("SMGAmmo")){
            transform.BroadcastMessage("MoreAmmoPlease", 5);
            Destroy(col.transform.gameObject);
        }
		if (col.transform.name.Contains("HealthPack")){
            transform.BroadcastMessage("WeNeedHealing", 6);
            Destroy(col.transform.gameObject);
        }
		if (col.transform.name.Contains("ArmorPack")){
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
