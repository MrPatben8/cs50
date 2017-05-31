using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootAnimation : MonoBehaviour {

	public Image Img;
	public Sprite[] spr;
	public float FrameInterval;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			StartCoroutine(Shoot());
		}
	}

	IEnumerator Shoot(){
		for(int i = 1; i < 6; i++){
			Img.sprite = spr[i];
			yield return new WaitForSeconds(FrameInterval);
		}
		Img.sprite = spr[0];
	}
}
