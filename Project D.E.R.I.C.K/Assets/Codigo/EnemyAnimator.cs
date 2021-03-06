﻿using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {

	// Use this for initialization
	public float Fps;
	public bool[] Animation;
	public Sprite[] WalkForward;
	public Sprite[] DeathAnim;
	public Sprite ShootinSprite;
	public SpriteRenderer rend;
	public Sprite Static;
	private bool dead = false;
	private bool Shooting = false;
	public AudioSource stepaud;
	public AudioSource shotaud;
	public AudioClip shot;
	public AudioClip Died;
	private bool Diededed = true;

	private Vector3 oldpos;
	private Vector3 newpos;
	void Start () {
		StartCoroutine(Anim());
	}

	void Update(){
		if (Time.timeScale == 0)
			stepaud.mute = true;

		newpos = transform.position;
		if (Vector3.Distance (oldpos, newpos) < 0.01f && !dead) {
			Animation [0] = false;
			stepaud.mute = true;
		} else {
			Animation [0] = true;
			stepaud.mute = false;
		}
		if (dead) {
			stepaud.loop = false;
			stepaud.clip = Died;
			if (Diededed) {
				stepaud.Play ();
				Diededed = false;
			}
		}
		oldpos = newpos;
	}

	// Update is called once per frame
	IEnumerator Anim(){
		if(Animation[0]){
			for(int i =0; i<WalkForward.Length;i++){
				rend.sprite = WalkForward[i];
				yield return new WaitForSeconds(Fps);
			}
		}
		yield return new WaitForEndOfFrame();
		if(!Shooting)
			rend.sprite = Static;
		if(!dead)
			StartCoroutine(Anim());
	}

	public void ShotFired(){
		StartCoroutine(AnimateFire());
	}

	IEnumerator AnimateFire(){
		if(!dead){
			Shooting = true;
			rend.sprite = ShootinSprite;
			if(shotaud.isPlaying)shotaud.Stop();	shotaud.clip = shot;	shotaud.Play();
			yield return new WaitForSeconds(Fps*2);
			Shooting = false;
		}
	}

	public void Animate(bool val){
		if(!dead){
			//Animation[0] = val;
		}
	}
	public void Die(){
		StartCoroutine(Death());
	}

	IEnumerator Death(){
		dead = true;
		StopCoroutine(Anim());
		for(int i =0; i<DeathAnim.Length;i++){
			rend.sprite = DeathAnim[i];
			yield return new WaitForSeconds(Fps);
		}
		transform.parent.GetComponent<NavMeshAgent>().stoppingDistance = 99999999.9f;
	}
}
