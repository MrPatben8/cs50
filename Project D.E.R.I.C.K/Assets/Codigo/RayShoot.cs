using UnityEngine;
using System.Collections;

public class RayShoot : MonoBehaviour {

	// Use this for initialization
	public GameObject Canvas;
	public int Ammo = 10;
	private int PistolDmg = 10;
	private bool ShotReady = true;
	private bool ReloadReady = true;
	public float FireRate;
	public float ReloadRate;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward*20, Color.red);
		if (Input.GetMouseButtonDown(0) && Ammo > 0 && ShotReady) {
			StartCoroutine(Fire());
		}
		if(Input.GetKeyDown(KeyCode.R) && ReloadReady){
			StartCoroutine(Reload());
		}
		if(Ammo <= 0 && ReloadReady){
			StartCoroutine(Reload());
		}
	}

	IEnumerator Reload(){
		ReloadReady = false;
		yield return new WaitForSeconds(ReloadRate);
		Ammo = 10;
		ReloadReady = true;
	}

	IEnumerator Fire(){
		ShotReady = false;
		Canvas.BroadcastMessage("Shoot");
		Ammo -= 1;
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hit)){
			if (hit.collider.transform.tag == "Enemy"){
				hit.collider.transform.gameObject.SendMessage("TakeDamage", PistolDmg);
			}
		}
		yield return new WaitForSeconds(FireRate);
		ShotReady = true;
	}
}
