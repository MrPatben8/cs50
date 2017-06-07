using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject NewLvl;
	public GameObject OldLvl;
	private BoxCollider cbox;
	void Start () {
		cbox = GetComponent<BoxCollider>();
		NewLvl.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Player"){
			OldLvl.SetActive(false);
			NewLvl.SetActive(true);
			StartCoroutine(Solidify());
		}
	}

	IEnumerator Solidify(){
		yield return new WaitForSeconds(5.0f);
		cbox.isTrigger = false;
	}
}
