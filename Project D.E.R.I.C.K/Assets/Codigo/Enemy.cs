using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	public int Health = 100;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.one * (float)(Health/100.0f);
	}

	public void TakeDamage(int dmg){
		Health -= dmg;
	}
}
