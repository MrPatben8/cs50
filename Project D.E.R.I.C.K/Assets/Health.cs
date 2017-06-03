using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Use this for initialization
	public int HP;
	public int SH;
	public AudioClip DamageSound;
	private AudioSource aud;
	void Start () {
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int dmg){
		HP -= dmg;
		if(aud.isPlaying){aud.Stop();}
		aud.clip = DamageSound;
		aud.Play();
	}
}
