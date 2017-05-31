using UnityEngine;
using System.Collections;

public class TextrueAdjust : MonoBehaviour {

	// Use this for initialization
	public int factor = 1;
	private MeshFilter mf;
	void Start () {
		mf = GetComponent<MeshFilter>();
		Bounds boun = mf.mesh.bounds;
		Vector3 size = Vector3.Scale(boun.size, transform.localScale) * factor;
		Vector3 newSize = new Vector3(size.x, size.z, size.z);
		if(size.y < 0.001f){
			size.y = size.z;
		}
		GetComponent<Renderer>().material.mainTextureScale = newSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
