using UnityEngine;
using System.Collections;

public class Barricada : MonoBehaviour {

	// Use this for initialization
	public GameObject[] Barricadas;
	void Start () {
		int rand = Random.Range(0, Barricadas.Length);
		for(int i =0; i < Barricadas.Length; i++){
			Barricadas[i].SetActive(false);
		}
		Barricadas[rand].SetActive(true);
		//Instantiate(Barricadas[rand], transform.position, transform.rotation, transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
