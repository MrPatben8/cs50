using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	// Use this for initialization
	public Image HpUI;
	public Image ShUI;
	public Health PlyrH;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HpUI.fillAmount = (float)(PlyrH.HP /100.0f);
		ShUI.fillAmount = (float)(PlyrH.SH /100.0f);
	}
}
