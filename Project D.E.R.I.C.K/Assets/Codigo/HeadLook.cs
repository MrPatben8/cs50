using UnityEngine;
using System.Collections;

public class HeadLook : MonoBehaviour {

	public GameObject PlayerCam;
	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Lerp(transform.rotation, PlayerCam.transform.rotation, Time.time * speed);
	}
}
