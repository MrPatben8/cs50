using UnityEngine;
using System.Collections;

public class MusicSelector : MonoBehaviour {

	public AudioSource aud;
	public AudioClip[] clip;
	// Use this for initialization
	void Start () {
		aud.clip = clip[Random.Range(0, clip.Length)];
		aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeSong(int num){
		if(aud.isPlaying)
			aud.Stop();
		aud.clip = clip[num];
		aud.Play();
	}
}
