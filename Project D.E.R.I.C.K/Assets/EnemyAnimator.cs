using UnityEngine;
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
	void Start () {
		StartCoroutine(Anim());
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
			Animation[0] = val;
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
