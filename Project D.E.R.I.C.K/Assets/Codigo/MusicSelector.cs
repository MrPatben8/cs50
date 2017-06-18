using UnityEngine;
using System.Collections;

public class MusicSelector : MonoBehaviour {

	public AudioSource aud;
	public AudioClip[] clip;
	public AudioClip MackingtoshPlus;
	private float songtime;
	private AudioClip oldsong;
	// Use this for initialization
	void Start () {
		aud.clip = clip[Random.Range(0, clip.Length)];
		aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.name == "420Zone") {
			songtime = aud.time;
			oldsong = aud.clip;
			aud.time = 0.0f;
			aud.clip = MackingtoshPlus;
			aud.Play ();
			RenderSettings.fogColor = new Color (1.0f, 0.39f, 1.0f);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.transform.name == "420Zone") {
			aud.clip = oldsong;
			aud.time = songtime;
			aud.Play ();
			RenderSettings.fogColor = Color.black;
		}
	}

	void ChangeSong(int num){
		if(aud.isPlaying)
			aud.Stop();
		aud.clip = clip[num];
		aud.Play();
	}
}
