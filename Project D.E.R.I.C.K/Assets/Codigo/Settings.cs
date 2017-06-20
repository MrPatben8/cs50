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
		if(PlayerPrefs.GetFloat("SFX") == null)
			PlayerPrefs.SetFloat ("SFX", 0.0f);
		if(PlayerPrefs.GetFloat("Bright") == null)
			PlayerPrefs.SetFloat ("Bright", 0.4f);
		if(PlayerPrefs.GetFloat("Music") == null)
			PlayerPrefs.SetFloat ("Music", 0.0f);
		
		SFX.value = PlayerPrefs.GetFloat ("SFX");
		Bright.value = PlayerPrefs.GetFloat ("Bright");
		Music.value = PlayerPrefs.GetFloat ("Music");
	}

	public void Sett(){
		setti = !setti;
		PlayerPrefs.SetFloat ("SFX", SFX.value);
		PlayerPrefs.SetFloat ("Bright", Bright.value);
		PlayerPrefs.SetFloat ("Music", Music.value);
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
