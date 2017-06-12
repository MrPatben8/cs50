using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	public bool Paused;
	public GameObject Menu;
	public AudioClip Song;
	public AudioSource aud;
	private AudioClip tempsong;
	private float temptime;
	private float pausetime = 5.0f;
	public AudioMixer mixer;
	void Start () {
		//aud = GetComponent<AudioSource> ();
		//tempsong = aud.clip;
	}

	public void ChangePause(){
		Paused = !Paused;
	}

	public void Restart(){
		SceneManager.UnloadScene ("Main");
		SceneManager.LoadScene ("Main");
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
				aud.time = pausetime;
				aud.Play ();
			}
			mixer.SetFloat ("SFXGain", 0.0f);
			Menu.SetActive (true);
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		} else {
			Time.timeScale = 1.0f;
			if (aud.clip == Song) {
				pausetime = aud.time;
				aud.Stop ();
				aud.clip = tempsong;
				aud.time = temptime;
				aud.Play ();
			}
			Menu.SetActive (false);
		}
	}
}
