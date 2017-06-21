using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	// Use this for initialization
	public GameObject Explosion;
	public AudioSource aud;
	private bool Exploded;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player" && !Exploded) {
			Exploded = true;
			col.SendMessageUpwards ("TakeDamage", Random.Range(10,25));
			Instantiate (Explosion, transform.position, Quaternion.identity);
			aud.Play ();
			gameObject.GetComponentInChildren<MeshRenderer> ().enabled = false;
			Destroy (this.gameObject, 1.0f);
		}
	}
}
