using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RayShoot : MonoBehaviour {
	/*
	1: Pistol Ammo
	2: Shotgun
	3: Shotgun Ammo
	4: SMG
	5: SMG Ammo
	6: Medkit
	7: Armor
	*/

	// Use this for initialization
	public GameObject Canvas;	//This variable references the canvas where the guns are displayed
	public int SW;				//This variable indicates which gun the player has in hand
	public AudioSource aud;		//This is the audiosource where all the gun sounds are played through
	public AudioClip EmptySound;//This sound is played if the player runs out of ammo
	public AudioClip PickupSound;//This sound is played when the player picks up a new gun
	public AudioClip DyingSound;
	public Transform[] spread;	//This array contains all the positions where the shotgun will fire in
	public GameObject BulletHit;
	public GameObject EnemyHit;//This is the gameobject that is spawned when the bullet hits a wall
	public LayerMask lyrmsk;	//This is the layers that the raycast will ignore
	public Text AmmoText;
	public Image[] WeaponIndicator;
	public Text WeaponText;
	private Health lieben;
	private bool Ded = true;

	public List<WeaponInfo> Weapon;  //Creates struct of weapon types
	[System.Serializable]
	public class WeaponInfo{
		public GameObject WeaponUI; 	//The Actual gun sprite
		public bool Available;			//Does the player have this weapon unlocked?
		public int MagSize;				//How many times can the player shoot before having to reload
		public int Ammo;				//How much ammo does the player have in the gun (Magsize - times shot)
		public int TotalAmmo;			//How much spare ammo the player has
		public int Damage;				//How much damage the gun does
		public float FireRate;			//time in seconds between shots
		public float ReloadRate;		//time in seconds of how long it takes to reload
		public bool ShotReady = true;	//Is the gun ready to fire (this controls the fire rate)
		public bool ReloadReady = true; //is the gun reloading?
		public AudioClip ShootSound;	//Shooting sound audio
		public AudioClip ReloadSound;	//reloadid sound audio
	}

	void Start(){
		lieben = transform.parent.parent.GetComponent<Health> ();
	}

	public void Pickup (int item) {				//funcion that unlocks a new weapon when called and automatically selects that weapon
		if(aud.isPlaying){	aud.Stop();	}		//}
		aud.clip = PickupSound;					// Plays a gun unlocked sound
		aud.Play();								//}
		if(item == 2){
			Weapon[1].Available = true;			//Unlocks gun and switches to it
			Switch(1);
		}
		if(item == 4){
			Weapon[2].Available = true;			//Unlocks gun and switches to it
			Switch(2);
		}
			
	}
	public void Switch (int item){				//function to call the corutine to swap weapons
		if(!Weapon[item].Available || !Weapon[SW].ReloadReady || !Weapon[SW].ShotReady || item == SW)
			return;
		StartCoroutine(WeaponSwap(item));
	}

	IEnumerator WeaponSwap(int item){			//corutine to swap weapons
		Weapon[SW].ShotReady = false;			//Stops the player from shooting
		Weapon[SW].ReloadReady = false;			//Stops the player from reloading;
		StartCoroutine(MoveDown());				//calls the corutine to lower the current weapon
		yield return new WaitForSeconds(0.3f);	//Waits .3 seconds
		SW = item;								//Sets the Selected Weapon (SW) to the new value
		StartCoroutine(MoveUp());				//Moves the SW up into screen
		yield return new WaitForSeconds(0.3f);	//Waits .3 seconds
		Weapon[SW].ShotReady = true;			//Allows shooting
		Weapon[SW].ReloadReady = true;			//Allows reloading
	}


	IEnumerator MoveDown(){						//Corutine to lower the weapon 50 pixels every 0.05 seconds
		Vector3 newpos = Weapon[SW].WeaponUI.GetComponent<RectTransform>().anchoredPosition3D;	//Creates new temporary Vector3 variable and sets it to the Weapon's position
		RectTransform rect = Weapon[SW].WeaponUI.GetComponent<RectTransform>();					//Creates reference to component and sets it to the Wapon's RectTransform component
		yield return new WaitForSeconds(0.05f);
		newpos.y -= 50;							//Lowers the temporary Vector 3 by 50 units (in this case pixels)
		rect.anchoredPosition3D = newpos;		//Applies the temporary vector to the Weapon, making it lower by 50 pixls
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
		if (lieben.HP <= 0 && Ded) {
			Ded = false;
			Weapon [SW].ShotReady = false;
			Weapon [SW].ReloadReady = false;
			StartCoroutine (MoveDown ());
			aud.clip = DyingSound;
			aud.Play ();
		}

		if (Time.timeScale == 0)
			aud.Pause ();
		else
			aud.UnPause ();
		if(Time.timeScale == 0)
			return;
		AmmoText.text = ""+Weapon[SW].Ammo+"/"+Weapon[SW].TotalAmmo;
		WeaponText.text = ""+(SW + 1)+"/"+Weapon.Count;
		for(int v=0; v < WeaponIndicator.Length; v++){
			WeaponIndicator[v].gameObject.SetActive(false);
		}
		WeaponIndicator[SW].gameObject.SetActive(true);
		if(Input.GetKeyDown(KeyCode.Alpha1)){	//Change to weapon 0 (Run Switch funcion) when the button 1 is pressed
			Switch(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Switch(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Switch(2);
		}

		Debug.DrawRay(transform.position, transform.forward*20, Color.red); //Draws a red line in editor to show where the player is aiming. This has no practical use
		if (Input.GetMouseButtonDown(0) && Weapon[SW].ShotReady) {			//If the player left clicks and the weapon is ready to shoot then start corutine Fire
			StartCoroutine(Fire());
		}
		if(Input.GetKeyDown(KeyCode.R) && Weapon[SW].ReloadReady && Weapon[SW].TotalAmmo > 0 && Weapon[SW].Ammo != Weapon[SW].MagSize){ //If player presses R AND the weapon is ready to reload AND the weapon is not full of ammo then start corutine Reload
			StartCoroutine(Reload());
		}
		if(Weapon[SW].Ammo <= 0 && Weapon[SW].ReloadReady && Weapon[SW].TotalAmmo > 0){	//If weapon's ammo reaches 0 AND the weapons reload is ready AND the weapons ammo is >0 the star corutine reload
			StartCoroutine(Reload());
		}

	}

	IEnumerator Reload(){ //Reloads the SW ammo
		if(Weapon[SW].TotalAmmo <= 0)	//if the weapon has no ammo then return nothins (Stop the execution of corutine)
			yield break;
		Weapon[SW].ReloadReady = false;	//Stop the player from shooting
		Weapon[SW].ShotReady = false;	//Stop the player from reloading again
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(MoveDown());		//Move the gun down
		if(aud.isPlaying){				
			aud.Stop();					//Play reloading sound
		}
		aud.clip = Weapon[SW].ReloadSound;
		aud.Play();

		yield return new WaitForSeconds(Weapon[SW].ReloadRate-0.6f);	//Wait for wepon's reloadrate
		if(Weapon[SW].TotalAmmo >= Weapon[SW].MagSize){
			Weapon[SW].TotalAmmo -= (Weapon[SW].MagSize - Weapon[SW].Ammo);
			Weapon[SW].Ammo = Weapon[SW].MagSize;
		}else{
			Weapon[SW].TotalAmmo -= (Weapon[SW].MagSize - Weapon[SW].Ammo);
			Weapon[SW].Ammo += (Weapon[SW].MagSize - Weapon[SW].Ammo);
		}
		StartCoroutine(MoveUp());
		yield return new WaitForSeconds(0.4f);
		Weapon[SW].ReloadReady = true;
		Weapon[SW].ShotReady = true;
	}

	IEnumerator Fire(){
		if(Weapon[SW].Ammo <= 0){ //if weapon is out of ammo play no ammo sound and stop corutine exectuion
			if(Weapon[SW].ReloadReady){
				if(aud.isPlaying){	aud.Stop();	}
				aud.clip = EmptySound;
				aud.Play();
			}
			yield break;
		}
		Weapon[SW].ShotReady = false;	//Stops player form firing again

		if(aud.isPlaying){	aud.Stop();	}
		aud.clip = Weapon[SW].ShootSound;	//Plays weapon fire sound
		aud.Play();

		Canvas.BroadcastMessage("Shoot");	//Tells the Gun sprite to play animation
		Weapon[SW].Ammo -= 1;				//Reduce ammo by 1
		RaycastHit hit;						//create RaycastHit variable
		Ray ray = new Ray(transform.position, transform.forward); //Draws a ray starting from this object to the front of the object
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, lyrmsk)){ //if the ray hits something output the data of that hit to the variable hit
			if (hit.collider.transform.tag == "Enemy"){				//if the ray hits an object with tag Enemy then tell the Enemy to take damage
				hit.collider.transform.gameObject.SendMessage("TakeDamage", Weapon[SW].Damage); //sends damage mesage to the gameobject of the collider that the ray hit
				GameObject enhit =  (GameObject)Instantiate(EnemyHit, hit.point, Quaternion.LookRotation(hit.normal)); //if the ray hits anything else then spawn a bullet hole
				enhit.transform.position = enhit.transform.position + hit.normal/20;
			}else{
				GameObject bulhit =  (GameObject)Instantiate(BulletHit, hit.point, Quaternion.LookRotation(hit.normal)); //if the ray hits anything else then spawn a bullet hole
				bulhit.transform.position = bulhit.transform.position + hit.normal/20;	
			}
		}
		if(SW == 1){ //if the weapon is the shotgun then draw 4 more rays
			for(int i = 0; i < spread.Length; i++){
				RaycastHit hit2;
				Vector3 dir = (spread[i].position - transform.position).normalized;
				Ray ray2 = new Ray(transform.position, dir);
				if (Physics.Raycast(ray2, out hit2, Mathf.Infinity, lyrmsk)){
					if (hit2.collider.transform.tag == "Enemy"){
						hit2.collider.transform.gameObject.SendMessage("TakeDamage", Weapon[SW].Damage);
						GameObject enhit =  (GameObject)Instantiate(EnemyHit, hit.point, Quaternion.LookRotation(hit.normal)); //if the ray hits anything else then spawn a bullet hole
						enhit.transform.position = enhit.transform.position + hit.normal/20;
					}else{
						GameObject bulhit =  (GameObject)Instantiate(BulletHit, hit2.point, Quaternion.LookRotation(hit2.normal));
						bulhit.transform.position = bulhit.transform.position + hit.normal/20;
					}
				}
			}
		}
		yield return new WaitForSeconds(Weapon[SW].FireRate);
		Weapon[SW].ShotReady = true;
		if(SW == 2 && Input.GetMouseButton(0)){ //if the weapon is the SMG then the corutine calls itself if the mouse button is held down
			StartCoroutine(Fire());	
		}
	}
}
