using UnityEngine;
using System.Collections;

public class RandomObjectSelector : MonoBehaviour {

	// Use this for initialization
	public GameObject[] Objects;
	public GameObject tempMesh;
	void Start () {
		int n = Random.Range(0, Objects.Length);
		Instantiate(Objects[n], tempMesh.transform.position, transform.rotation, transform);
		Destroy(tempMesh);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
