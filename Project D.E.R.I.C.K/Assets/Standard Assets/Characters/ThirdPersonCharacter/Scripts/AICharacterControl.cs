using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
		public float DetectionRange;
		public GameObject EnemySprite;
		public bool Moving;

		private float oldspd;
        private void Start()
        {

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
			oldspd = agent.speed;

			if(target == null){
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);
			float dist = Vector3.Distance(transform.position, target.position);
			if(dist < DetectionRange){
				Moving = true;
				EnemySprite.SendMessage("Animate", true);
				agent.speed = oldspd;
				BroadcastMessage("InRange");
			}else{
				Moving = false;
				EnemySprite.SendMessage("Animate", false);
				agent.speed = 0;
			}

			if(dist < DetectionRange && agent.remainingDistance > agent.stoppingDistance){
				GetComponentInChildren<AudioSource>().mute = false;
			}else{
				GetComponentInChildren<AudioSource>().mute = true;
			}

			if (agent.remainingDistance > agent.stoppingDistance){
				//character.Move(agent.desiredVelocity, false, false);
				EnemySprite.SendMessage("Animate", true);
				BroadcastMessage("Moving", true);
				gameObject.BroadcastMessage("ReadyFire", false);
			}else{
				//character.Move(Vector3.zero, false, false);
				EnemySprite.SendMessage("Animate", false);
				BroadcastMessage("Moving", false);
				gameObject.BroadcastMessage("ReadyFire", true);
			}
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
