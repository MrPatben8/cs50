using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Settings : MonoBehaviour {

	// Use this for initialization
	public GameObject UISetting;
	public GameObject UIMenu;
	private bool setti;
	public Slider SFX;
	public Slider Music;
	public Slider Bright;
	public AudioMixer mix;
	void Start () {
	
	}

	public void Sett(){
		setti = !setti;
	}
	
	// Update is called once per frame
	void Update () {
		if (setti) {
			UISetting.SetActive (true);
			UIMenu.SetActive (false);
		} else {
			UISetting.SetActive (false);
			UIMenu.SetActive (true);
		}
		float br = Bright.value;
		RenderSettings.ambientLight = new Color (br, br, br);
		mix.SetFloat ("SFXGain",(float)SFX.value);
		mix.SetFloat ("MusicGain",(float)Music.value);
	}
}
