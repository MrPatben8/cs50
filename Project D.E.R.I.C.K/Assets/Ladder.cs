using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	public bool Climb;
	public Vector3 FinalPos;
	public Vector3 StartPos;
	public float ClimbSpeed;
	public Rigidbody rig;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Climb) {
			//transform.localPosition = Vector3.Lerp (transform.localPosition, FinalPos, Time.deltaTime*ClimbSpeed);
			rig.MovePosition(Vector3.Lerp (transform.localPosition, FinalPos, Time.deltaTime * ClimbSpeed));
		} else {
			//transform.localPosition = Vector3.Lerp (transform.localPosition, StartPos, Time.deltaTime * ClimbSpeed);
			rig.MovePosition(Vector3.Lerp (transform.localPosition, StartPos, Time.deltaTime*ClimbSpeed));
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			Climb = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.transform.tag == "Player") {
			Climb = false;
		}
	}
}
