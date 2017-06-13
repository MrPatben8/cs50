using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	public int Health = 100;
	public int LowDamage = 5;
	public int HiDamage = 20;
	public float Acuracy;
	private float tempAcu;
	public bool Move;
	private bool dead;
	private GameObject plyr;
	private bool fire;
	private bool inrange;
	public GameObject Head;
	public GameObject HeadAim;
	private RaycastHit hit;
	private float shotintlow;
	private float shotinthi;
	public LayerMask Penetration;

	public float DifficultyLevel;
	public float MaxDifficultyDistance = 100;
	public List<Diffy> Difficulty;  
	[System.Serializable]
	public class Diffy{
		public float MinAcuracy = 50;
		public float MaxAcuracy = 100;
		public float MinAimSpeed = 0.5f;
		public float MaxAimSpeed = 0.90f;
		public float MinMoveSpeed = 5.0f;
		public float MaxMoveSpeed = 8.0f;
		public float MinShotInterval =0.5f;
		public float MaxShotInterval =4.0f;
	}

	void Start () {
		tempAcu = Acuracy;
		if(Acuracy > 100)
			Acuracy = 100;
		dead = false;
		plyr = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(CycleFire());
	}
	public void InRange(){
		inrange = true;
	}

	IEnumerator CycleFire(){
		if(inrange){
			//RaycastHit hit;
			Vector3 dir = (HeadAim.transform.position - Head.transform.position).normalized;
			Ray ray = new Ray(Head.transform.position, dir/*Head.transform.forward*/);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, Penetration)){
				if (hit.collider.transform.tag == "Player"){
					BroadcastMessage("ShotFired");
					float accu = Random.Range(0.01f, 99.9f);
					if(Acuracy == tempAcu && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)))
						Acuracy = Acuracy * Random.Range(0.7f, 1.0f);
					else
						Acuracy = tempAcu;
					if(accu <= Acuracy)
						hit.collider.transform.gameObject.SendMessage("TakeDamage", Random.Range(LowDamage, HiDamage)); 
				}
			}
			Debug.Log (hit.transform.name);
		}
		yield return new WaitForSeconds(Random.Range(shotintlow, shotinthi));
		if(Health > 0)
			StartCoroutine(CycleFire());
	}

	public void Moving(bool dat){
		Move = dat;
	}

	public void ReadyFire(bool dat){
		fire = dat;
	}

	// Update is called once per frame
	void Update () {
		CalculateDifficulty ();
		Vector3 dir = (HeadAim.transform.position - Head.transform.position).normalized;
		Debug.DrawRay(Head.transform.position, /*Head.transform.forward*10*/dir*20, Color.magenta);
		Debug.DrawLine (Head.transform.position, hit.point, Color.cyan);
	}

	public void TakeDamage(int dmg){
		Health -= dmg;
		if(Health <= 0 && !dead){
			BroadcastMessage("Die");
			GetComponentInChildren<AudioSource>().mute = true;
			dead=true;
			gameObject.layer = 9;
			gameObject.GetComponent<NavMeshAgent>().speed = 0;
			gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			gameObject.GetComponent<CapsuleCollider>().enabled = false;
		}
	}

	void CalculateDifficulty(){
		DifficultyLevel = (Vector3.Distance (transform.position, Vector3.zero) / MaxDifficultyDistance);

		shotintlow = Difficulty [0].MinShotInterval / (1.0f-DifficultyLevel);
		shotinthi = Difficulty [0].MaxShotInterval / (1.0f-DifficultyLevel);
		Acuracy = (Difficulty [0].MaxAcuracy - Difficulty [0].MinAcuracy) * DifficultyLevel + Difficulty [0].MinAcuracy;
		Head.GetComponent<LookatTarget> ().m_FollowSpeed = (Difficulty[0].MaxAimSpeed - Difficulty[0].MinAimSpeed)*DifficultyLevel + Difficulty[0].MinAimSpeed;
		gameObject.GetComponent<NavMeshAgent> ().speed = (Difficulty[0].MaxMoveSpeed - Difficulty[0].MinMoveSpeed)*DifficultyLevel + Difficulty[0].MaxMoveSpeed;
	}
}
