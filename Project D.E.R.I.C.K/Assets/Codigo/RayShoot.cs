using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayShoot : MonoBehaviour {

	// Use this for initialization
	public GameObject Canvas;
	public int SW;
	public AudioSource aud;
	public AudioClip EmptySound;
	public Transform[] spread;
	public GameObject BulletHit;
	public LayerMask lyrmsk;

	public List<WeaponInfo> Weapon;
	[System.Serializable]
	public class WeaponInfo{
		public GameObject WeaponUI;
		public bool Available;
		public int MagSize;
		public int Ammo;
		public int TotalAmmo;
		public int Damage;
		public float FireRate;
		public float ReloadRate;
		public bool ShotReady = true;
		public bool ReloadReady = true;
		public AudioClip ShootSound;
		public AudioClip ReloadSound;
	}

	public void Pickup (int item) {
		if(item == 2){
			Weapon[1].Available = true;
			Switch(1);
		}
			
	}
	public void Switch (int item){
		if(!Weapon[item].Available || !Weapon[SW].ReloadReady || !Weapon[SW].ShotReady || item == SW)
			return;
		StartCoroutine(WeaponSwap(item));
	}

	IEnumerator WeaponSwap(int item){
		Weapon[SW].ShotReady = false;
		Weapon[SW].ReloadReady = false;
		StartCoroutine(MoveDown());
		yield return new WaitForSeconds(0.3f);
		for(int i = 0; i < Weapon.Count; i++){
			//Weapon[i].WeaponUI.SetActive(false);
		}
		//Weapon[item].WeaponUI.SetActive(true);
		SW = item;
		StartCoroutine(MoveUp());
		yield return new WaitForSeconds(0.3f);
		Weapon[SW].ShotReady = true;
		Weapon[SW].ReloadReady = true;
	}


	IEnumerator MoveDown(){
		Vector3 newpos = Weapon[SW].WeaponUI.GetComponent<RectTransform>().anchoredPosition3D;
		RectTransform rect = Weapon[SW].WeaponUI.GetComponent<RectTransform>();
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;
		rect.anchoredPosition3D = newpos;
	}

	IEnumerator MoveUp(){
		Vector3 newpos = Weapon[SW].WeaponUI.GetComponent<RectTransform>().anchoredPosition3D;
		RectTransform rect = Weapon[SW].WeaponUI.GetComponent<RectTransform>();
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
		yield return new WaitForSeconds(0.05f);
		newpos.y += 50;
		rect.anchoredPosition3D = newpos;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Switch(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Switch(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Switch(2);
		}

		Debug.DrawRay(transform.position, transform.forward*20, Color.red);
		if (Input.GetMouseButtonDown(0) && Weapon[SW].ShotReady) {
			StartCoroutine(Fire());
		}
		if(Input.GetKeyDown(KeyCode.R) && Weapon[SW].ReloadReady && Weapon[SW].TotalAmmo > 0 && Weapon[SW].Ammo != Weapon[SW].MagSize){
			StartCoroutine(Reload());
		}
		if(Weapon[SW].Ammo <= 0 && Weapon[SW].ReloadReady && Weapon[SW].TotalAmmo > 0){
			StartCoroutine(Reload());
		}

	}

	IEnumerator Reload(){
		if(Weapon[SW].TotalAmmo <= 0)
			yield break;
		Weapon[SW].ReloadReady = false;
		Weapon[SW].ShotReady = false;
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(MoveDown());
		if(aud.isPlaying){
			aud.Stop();
		}
		aud.clip = Weapon[SW].ReloadSound;
		aud.Play();

		yield return new WaitForSeconds(Weapon[SW].ReloadRate-0.6f);
		if(Weapon[SW].TotalAmmo >= Weapon[SW].MagSize){
			Weapon[SW].TotalAmmo -= (Weapon[SW].MagSize - Weapon[SW].Ammo);
			Weapon[SW].Ammo = Weapon[SW].MagSize;
		}else{
			Weapon[SW].TotalAmmo -= (Weapon[SW].MagSize - Weapon[SW].Ammo);
			Weapon[SW].Ammo += (Weapon[SW].MagSize - Weapon[SW].Ammo);
		}
		Weapon[SW].ReloadReady = true;
		Weapon[SW].ShotReady = true;
		StartCoroutine(MoveUp());
	}

	IEnumerator Fire(){
		if(Weapon[SW].Ammo <= 0){
			if(Weapon[SW].ReloadReady){
				if(aud.isPlaying){	aud.Stop();	}
				aud.clip = EmptySound;
				aud.Play();
			}
			yield break;
		}
		Weapon[SW].ShotReady = false;

		if(aud.isPlaying){	aud.Stop();	}
		aud.clip = Weapon[SW].ShootSound;
		aud.Play();

		Canvas.BroadcastMessage("Shoot");
		Weapon[SW].Ammo -= 1;
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, lyrmsk)){
			if (hit.collider.transform.tag == "Enemy"){
				hit.collider.transform.gameObject.SendMessage("TakeDamage", Weapon[SW].Damage);
			}else{
				GameObject bulhit =  (GameObject)Instantiate(BulletHit, hit.point, Quaternion.LookRotation(hit.normal));
				bulhit.transform.position = new Vector3(bulhit.transform.position.x, bulhit.transform.position.y + 0.1f, bulhit.transform.position.z);
			}
		}
		if(SW == 1){
			for(int i = 0; i < spread.Length; i++){
				RaycastHit hit2;
				Vector3 dir = (spread[i].position - transform.position).normalized;
				Ray ray2 = new Ray(transform.position, dir);
				if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, lyrmsk)){
					if (hit2.collider.transform.tag == "Enemy"){
						hit2.collider.transform.gameObject.SendMessage("TakeDamage", Weapon[SW].Damage);
					}else{
						GameObject bulhit =  (GameObject)Instantiate(BulletHit, hit2.point, Quaternion.LookRotation(hit2.normal));
						bulhit.transform.position = new Vector3(bulhit.transform.position.x, bulhit.transform.position.y + 0.1f, bulhit.transform.position.z);
					}
				}
			}
		}
		yield return new WaitForSeconds(Weapon[SW].FireRate);
		Weapon[SW].ShotReady = true;
		if(SW == 2 && Input.GetMouseButton(0)){
			StartCoroutine(Fire());
		}
	}
}
