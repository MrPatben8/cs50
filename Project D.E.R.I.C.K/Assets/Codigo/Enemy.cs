using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	public int Health = 100;
	public bool Move;
	private bool dead;
	void Start () {
		dead = false;
	}

	public void Moving(bool dat){
		Move = dat;
	}

	// Update is called once per frame
	void Update () {
		if(!Move){
			Debug.Log("Shoot");
		}
	}

	public void TakeDamage(int dmg){
		Health -= dmg;
		if(Health <= 0 && !dead){
			BroadcastMessage("Die");
			dead=true;
			gameObject.layer = 9;
		}
	}
}
