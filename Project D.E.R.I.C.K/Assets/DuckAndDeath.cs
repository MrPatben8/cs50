using UnityEngine;
using System.Collections;

public class DuckAndDeath : MonoBehaviour {

	private Health leben;
	public GameObject testa;
	public Vector3 Stand;
	public Vector3 Crouch;
	public Vector3 Dead;
	public float CrouchSpeed;
	public float DieSpeed = 1.2f;
	void Start () {
		leben =  gameObject.GetComponent<Health> ();
	}

	// Update is called once per frame
	void Update () {
		if (leben.HP > 0) {
			if (Input.GetKey (KeyCode.LeftControl)) {
				testa.transform.localPosition = Vector3.Lerp(testa.transform.localPosition, Crouch, Time.deltaTime * CrouchSpeed);
			} else {
				testa.transform.localPosition = Vector3.Lerp(testa.transform.localPosition, Stand, Time.deltaTime * CrouchSpeed);
			}
		} else {
			testa.transform.localPosition = Vector3.Lerp (testa.transform.localPosition, Dead, Time.deltaTime * DieSpeed);	
		}
	}
}