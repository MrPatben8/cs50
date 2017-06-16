using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DuckAndDeath : MonoBehaviour {

	private Health leben;
	public GameObject testa;
	public Vector3 Stand;
	public Vector3 Crouch;
	public Vector3 Dead;
	public float CrouchSpeed;
	public float DieSpeed = 1.2f;
	private float WalkSpeed;
	private FirstPersonController fir;
	void Start () {
		leben =  gameObject.GetComponent<Health> ();
		fir = gameObject.GetComponent<FirstPersonController> ();
		WalkSpeed = fir.NewWalkSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (leben.HP > 0) {
			if (Input.GetKey (KeyCode.LeftControl)) {
				testa.transform.localPosition = Vector3.Lerp(testa.transform.localPosition, Crouch, Time.deltaTime * CrouchSpeed);
				fir.NewWalkSpeed = (float)(WalkSpeed / 2.0f);
			} else {
				testa.transform.localPosition = Vector3.Lerp(testa.transform.localPosition, Stand, Time.deltaTime * CrouchSpeed);
				fir.NewWalkSpeed = WalkSpeed;
			}
		} else {
			testa.transform.localPosition = Vector3.Lerp (testa.transform.localPosition, Dead, Time.deltaTime * DieSpeed);	
		}
	}
}