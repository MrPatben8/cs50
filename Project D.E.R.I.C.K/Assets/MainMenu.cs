using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public AudioSource aud;
	public GameObject Title;
	private RectTransform rect;
	private RectTransform rect2;
	public bool flash = true;
	public GameObject[] Lights;
	public GameObject Boss;
	public GameObject Button;

	void Start(){
		RenderSettings.ambientLight = Color.gray;
		rect = Title.GetComponent<RectTransform> ();
		rect2 = Button.GetComponent<RectTransform> ();
		aud.time = 0;
		for (int i = 0; i < Lights.Length; i++)
			Lights [i].SetActive (false);
		Boss.SetActive (false);
	}

	void Update ()
	{
		if (aud.time < 15.5f && Input.anyKeyDown) {
			aud.time = 15.5f;
		}
		transform.RotateAround (transform.parent.position, Vector3.down, 0.5f);
		if (aud.time > 15.6f) { //15.6
			if (flash)
				StartCoroutine (Flash ());
			flash = false;
		}
		if (aud.time > 17.5f) {
			Debug.Log ("menu show");
			Vector3 newpos = Vector3.Lerp (rect.anchoredPosition3D, new Vector3 (0, -250, 0), Time.deltaTime * 5.0f);
			rect.anchoredPosition3D = newpos;

			Vector3 newpos2 = Vector3.Lerp (rect2.anchoredPosition3D, new Vector3 (0, -200, 0), Time.deltaTime * 5.0f);
			rect2.anchoredPosition3D = newpos2;
		}
	}

	IEnumerator Flash(){
		RenderSettings.ambientLight = Color.black;
		Boss.SetActive (true);
		float x = 0;
		while (x < 1.65f) {
			float c = Random.Range (0.01f, 0.2f);
			yield return new WaitForSeconds (c);
			x += c;
			int g = Random.Range (0, Lights.Length);
			int j = Random.Range (0, Lights.Length);
			Lights [g].SetActive (true);
			Lights [j].SetActive (true);
			yield return new WaitForSeconds (c);
			Lights [g].SetActive (false);
			Lights [j].SetActive (false);
			x += c;
		}
		Boss.SetActive (false);
		RenderSettings.ambientLight = Color.gray;
	}

	public void StartGame(){
		SceneManager.LoadScene ("Main");
	}
}
