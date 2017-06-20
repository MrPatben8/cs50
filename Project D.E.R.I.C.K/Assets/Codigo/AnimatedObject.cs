using UnityEngine;
using System.Collections;

public class AnimatedObject : MonoBehaviour {

	// Use this for initialization
	public bool Loop;
	public bool DestroyAfter;
	public Sprite[] spr;
	public int FPS;
	private float waittime;
	private SpriteRenderer rend;
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		waittime = (float)(1.0f / FPS);
		StartCoroutine(Animate());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Animate(){
		for(int i=0; i < spr.Length; i++){
			rend.sprite = spr[i];
			yield return new WaitForSeconds(waittime);
		}
		if(Loop)
			StartCoroutine(Animate());
		if(DestroyAfter)
			Destroy(this.gameObject);
	}
}
