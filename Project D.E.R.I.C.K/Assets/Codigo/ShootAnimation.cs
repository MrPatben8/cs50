using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootAnimation : MonoBehaviour {

	public Image Img;
	public Sprite[] spr;
	public float FrameInterval;

	public AudioSource aud;
	public AudioClip GunShot;

	public void Shoot () {
			StartCoroutine(Fire());
	}

	IEnumerator Fire(){
		if(aud.isPlaying){
			aud.Stop();
		}
		aud.clip = GunShot;
		aud.Play();
		for(int i = 1; i < 6; i++){
			Img.sprite = spr[i];
			yield return new WaitForSeconds(FrameInterval);
		}
		Img.sprite = spr[0];
	}
}
