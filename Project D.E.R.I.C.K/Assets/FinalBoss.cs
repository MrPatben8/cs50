using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class FinalBoss : MonoBehaviour {

	// Use this for initializatio
	public int TotalHP = 10000;
	public GameObject Cannon1;
	public GameObject Cannon2;
	public GameObject Muzzle1;
	public GameObject Muzzle2;
	public GameObject Arms;
	public LineRenderer Laser1;
	public LineRenderer Laser2;
	public LayerMask NonShootable;
	public TextMesh HpIndicator;
	public int VulnerabilityDamageMultiplier = 5;
	public int DamageToPlayer = 5;
	public GameObject BulletHit;
	public GameObject Explosion;

	public AudioSource SpinSource;
	public AudioSource ShootSource;

	public AudioClip SpinStatic;
	public AudioClip SpinUp;
	public AudioClip SpinShooting;
	public AudioClip SpinDown;
	public AudioClip ShootingSound;
	public AudioClip DeathExplosion;

	public LookatTarget BodyRotator;
	public LookatTarget ArmsRotator;

	public GameObject Shield;
	public GameObject ShieldUpPos;
	private bool Vulnerable;
	private bool BattleMode;
	private bool Spin;
	private int InitialHP;
	private RaycastHit PubHit;
	private bool Dead = false;
	void Start () {
		InitialHP = TotalHP;
		StartCoroutine (AttackMode());
		StartCoroutine (FireRate());
		SpinSource.Stop ();
		SpinSource.clip = SpinStatic;
		SpinSource.loop = true;
		SpinSource.Play ();	
	}

	IEnumerator AttackMode(){
		yield return new WaitForSeconds (Random.Range(5,15));
		SpinSource.Stop ();
		SpinSource.clip = SpinUp;
		Spin = true;
		SpinSource.Play ();
		yield return new WaitForSeconds (3.0f);
		BattleMode = true;
		SpinSource.Stop ();
		SpinSource.clip = SpinShooting;
		SpinSource.loop = true;
		SpinSource.Play ();	
		yield return new WaitForSeconds (Random.Range(5,10));
		BattleMode = false;											
		Vulnerable = true;
		SpinSource.Stop ();
		SpinSource.clip = SpinDown;
		SpinSource.loop = false;
		SpinSource.Play ();	
		yield return new WaitForSeconds (Random.Range(3,5));
		Vulnerable = false;
		Spin = false;
		SpinSource.Stop ();
		SpinSource.clip = SpinStatic;
		SpinSource.loop = true;
		SpinSource.Play ();	
		StartCoroutine (AttackMode ());
	}
	IEnumerator FireRate(){
		Shoot ();
		yield return new WaitForSeconds (1.0f/72.0f);
		StartCoroutine (FireRate());
	}

	void Shoot(){
		if (BattleMode && PubHit.transform != null && TotalHP>0) {
			ShootSource.Stop ();
			ShootSource.clip = ShootingSound;
			ShootSource.Play ();

			GameObject obj1 = (GameObject)Instantiate (Explosion, Muzzle1.transform.position, Muzzle1.transform.rotation);
			GameObject obj2 = (GameObject)Instantiate (Explosion, Muzzle2.transform.position, Muzzle2.transform.rotation);
			obj1.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			obj2.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			//obj1.GetComponent<AnimatedObject> ().FPS = 120;
			//obj2.GetComponent<AnimatedObject> ().FPS = 120;

			if (PubHit.transform.tag == "Player") {
				PubHit.transform.gameObject.SendMessage ("TakeDamage", DamageToPlayer);
			} else {
				GameObject bulhit =  (GameObject)Instantiate(BulletHit, PubHit.point, Quaternion.LookRotation(PubHit.normal), PubHit.transform); //if the ray hits anything else then spawn a bullet hole
				bulhit.transform.position = bulhit.transform.position + PubHit.normal/20;	
				bulhit.transform.localScale = new Vector3(1/PubHit.transform.localScale.x, 1/PubHit.transform.localScale.x, 1/PubHit.transform.localScale.z);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0 || TotalHP<=0) {
			SpinSource.mute = true;
		} else {
			SpinSource.mute = false;
		}
		HpIndicator.text = ""+TotalHP;
		if (Vulnerable) {
			Shield.transform.localPosition = Vector3.Lerp (Shield.transform.localPosition, new Vector3 (0f, 3f, 0.5f), Time.deltaTime * 2);
		} else {
			Shield.transform.localPosition = Vector3.Lerp (Shield.transform.localPosition, Vector3.zero, Time.deltaTime * 2);
		}

		BodyRotator.m_FollowSpeed = (float)((float)TotalHP/(float)InitialHP);
		ArmsRotator.m_FollowSpeed = (float)((float)TotalHP/(float)InitialHP);

		if (Spin && TotalHP>0) {
			Cannon1.transform.Rotate (Vector3.forward * Time.deltaTime * 360*2);
			Cannon2.transform.Rotate (Vector3.back * Time.deltaTime * 360*2);
		}

		if (BattleMode && TotalHP>0) {
			RaycastHit hit;
			Vector3 dir = (Arms.transform.forward - Arms.transform.position).normalized;
			Debug.DrawRay (Arms.transform.position, Arms.transform.forward * 50, Color.green);
			Ray ray = new Ray (Arms.transform.position, Arms.transform.forward);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, NonShootable)) {
				Debug.Log (hit.transform.name);
				PubHit = hit;
				Laser1.SetPosition (1, hit.point);
				Laser1.SetPosition (0, Cannon1.transform.position);
				Laser2.SetPosition (1, hit.point);
				Laser2.SetPosition (0, Cannon2.transform.position);
			}
		} else {
			Laser1.SetPosition (1, Vector3.zero);
			Laser1.SetPosition (0, Vector3.zero);
			Laser2.SetPosition (1, Vector3.zero);
			Laser2.SetPosition (0, Vector3.zero);
		}
	}

	void Explode(){
		if (!Dead) {
			Rigidbody[] rig = GetComponentsInChildren<Rigidbody> ();
			for (int i = 0; i < rig.Length; i++) {
				rig [i].isKinematic = false;
				Instantiate (Explosion, rig [i].transform.parent.position, Quaternion.identity);
			}

			BodyRotator.enabled = false;
			ArmsRotator.enabled = false;
			HpIndicator.gameObject.SetActive (false);

			ShootSource.Stop ();
			ShootSource.clip = DeathExplosion;
			ShootSource.Play ();

			Dead = true;
		}
	}

	public void TakeDamage(int num){
		if (TotalHP - num <= 0) {
			TotalHP = 0;
			Explode ();
		} else {
			if (Vulnerable) {
				TotalHP -= num * Random.Range (1, VulnerabilityDamageMultiplier);
			} else {
				TotalHP -= num / 2;
			}
		}
	}
}
