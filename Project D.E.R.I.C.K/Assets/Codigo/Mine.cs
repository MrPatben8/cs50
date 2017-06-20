using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	// Use this for initialization
	public GameObject Explosion;
	public AudioSource aud;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			col.SendMessageUpwards ("TakeDamage", Random.Range(10,25));
			Instantiate (Explosion, transform.position, Quaternion.identity);
			aud.Play ();
			Destroy (this.gameObject);
		}
	}
}
