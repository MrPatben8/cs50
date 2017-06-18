using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Health : MonoBehaviour {

	// Use this for initialization
	public int HP;
	public int SH;
	public AudioClip DamageSound;
	public AudioClip DyingSound;
	private AudioSource aud;
	private bool OneDead = true;
	public Image[] DamageUI;
	public GameObject DeathText;
	public GameObject UiHud;
    public int heil = 15;   //casi significa curación que es "heilen" XD
    public int SHIELD = 20; // es lo que se aniade al escudo
	void Start () {
		aud = GetComponent<AudioSource>();
		DeathText.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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

		if (HP <= 0) {
			gameObject.GetComponent<FirstPersonController> ().enabled = false;
			RectTransform vet = DeathText.GetComponent<RectTransform> ();
			vet.anchoredPosition3D = new Vector3(0, Mathf.Lerp (vet.anchoredPosition3D.y, 0, Time.deltaTime*1.5f), 0);
			DeathText.SetActive (true);
			UiHud.SetActive (false);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			if (OneDead) {
				aud.clip = DyingSound;
				aud.Play ();
				OneDead = false;
			}
		}
	}

	public void TakeDamage(int dmg){
        if (SH > 0)
        {
            if (SH - dmg >= 0)
            {
                SH -= dmg;
            }
            else
            {
                SH -= dmg;
                HP += SH;
                SH = 0;
            }

        }
            else { HP -= dmg; }

        if (aud.isPlaying){aud.Stop();}
		if (OneDead) {
			aud.clip = DamageSound;
			aud.Play ();
		}
	}
    public void WeNeedHealing(int item)
    {
        if (item == 6)
        {
            if (HP + heil <= 100)
            {
                HP += heil;
            }else
                {
                HP = 100;
                }
        }
        if(item == 7)
        {
            if(SH + SHIELD <= 100)
            {
                SH += SHIELD;
            }else
                {
                SH = 100;
                }
        }
    }

}
