using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	public bool Paused;
	public GameObject Menu;
	public AudioClip Song;
	public AudioSource aud;
	private AudioClip tempsong;
	private float temptime;
	void Start () {
		//aud = GetComponent<AudioSource> ();
		//tempsong = aud.clip;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Paused = !Paused;
		if (Paused) {
			Time.timeScale = 0.0f;
			if (aud.clip != Song) {
				tempsong = aud.clip;
				temptime = aud.time;
				aud.Stop ();
				aud.clip = Song;
				aud.Play ();
			}
			Menu.SetActive (true);
		} else {
			Time.timeScale = 1.0f;
			if (aud.clip == Song) {
				aud.Stop ();
				aud.clip = tempsong;
				aud.time = temptime;
				aud.Play ();
			}
			Menu.SetActive (false);
		}
	}
}
