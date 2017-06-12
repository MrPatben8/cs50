using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Health : MonoBehaviour {

	// Use this for initialization
	public int HP;
	public int SH;
	public AudioClip DamageSound;
	private AudioSource aud;
	public Image[] DamageUI;
	void Start () {
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (HP >= 90) {
			for (int i = 0; i < DamageUI.Length; i++) {
				Color col1 = DamageUI [i].color;
				col1.a = 0.0f;
				DamageUI [i].color = col1;
			}
		}
		if (HP < 90) {
			Color col1 = DamageUI [0].color;
			col1.a = 1.0f-(float)(HP / 90.0f);
			if (col1.a > 1.0f)
				col1.a = 1.0f;
			DamageUI [0].color = col1;
		}
		if (HP < 60) {
			Color col1 = DamageUI [1].color;
			col1.a = 1.0f-(float)(HP / 60.0f);
			if (col1.a > 1.0f)
				col1.a = 1.0f;
			DamageUI [1].color = col1;
		}
		if (HP < 30) {
			Color col1 = DamageUI [2].color;
			col1.a = 1.0f-(float)(HP / 30.0f);
			if (col1.a > 1.0f)
				col1.a = 1.0f;
			DamageUI [2].color = col1;
		}
	}

	public void TakeDamage(int dmg){
		HP -= dmg;
		if(aud.isPlaying){aud.Stop();}
		aud.clip = DamageSound;
		aud.Play();
	}
}
