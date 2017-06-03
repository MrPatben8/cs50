using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters;
using UnityStandardAssets.CrossPlatformInput;

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
			RaycastHit hit;
			Vector3 dir = (plyr.transform.position - transform.position).normalized;
			Ray ray = new Ray(transform.position, dir);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
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
		}
		yield return new WaitForSeconds(Random.Range(2.5f, 5f));
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
		Vector3 dir = (plyr.transform.position - transform.position).normalized;
		Debug.DrawRay(transform.position, dir, Color.red);
		if(!Move){
			Debug.Log("Shoot");
		}
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
}
