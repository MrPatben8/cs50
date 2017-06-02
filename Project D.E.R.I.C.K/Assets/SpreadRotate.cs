using UnityEngine;
using System.Collections;

public class SpreadRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			gameObject.transform.Rotate (0,0,30*Time.deltaTime); 
	}
}
