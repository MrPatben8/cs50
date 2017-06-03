using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	public GameObject Enemy;
	public float SpawnChance = 50.0f;
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().enabled=false;
		float ran = Random.Range(0.1f, 99.9f);
		if(ran < SpawnChance){
			Vector3 pos = transform.position;
			pos.y = 1.5f;
			GameObject ene = (GameObject)Instantiate(Enemy, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
