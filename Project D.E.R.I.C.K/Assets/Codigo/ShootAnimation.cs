using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootAnimation : MonoBehaviour {

	public Image Img;
	public Sprite[] spr;
	public float FrameInterval;

	public void Shoot () {
			StartCoroutine(Fire());
	}

	IEnumerator Fire(){
		for(int i = 1; i < spr.Length; i++){
			Img.sprite = spr[i];
			yield return new WaitForSeconds(FrameInterval);
		}
		Img.sprite = spr[0];
	}
}
