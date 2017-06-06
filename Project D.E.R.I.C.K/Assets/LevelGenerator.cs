using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject NewLvl;
	public GameObject OldLvl;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Player"){
			OldLvl.SetActive(false);
			NewLvl.SetActive(true);
		}
	}
}
