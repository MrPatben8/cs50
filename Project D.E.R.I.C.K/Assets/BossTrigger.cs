using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	// Use this for initialization
	public FinalBoss fib;
	void Start () {
		fib.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			fib.enabled = true;
			fib.PlaceMines ();
		}
		StartCoroutine (asdf());
	}
	IEnumerator asdf(){
		yield return new WaitForSeconds (2.0f);
		gameObject.GetComponent<BoxCollider> ().isTrigger = false;
	}
}

