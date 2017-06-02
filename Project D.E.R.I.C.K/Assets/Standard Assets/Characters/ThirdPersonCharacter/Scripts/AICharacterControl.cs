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


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);
			float dist = Vector3.Distance(transform.position, target.position);
			if(dist < DetectionRange){
				if (agent.remainingDistance > agent.stoppingDistance){
           	    	 character.Move(agent.desiredVelocity, false, false);
					EnemySprite.SendMessage("Animate", true);
					BroadcastMessage("Moving", true);
				}else{
             		 character.Move(Vector3.zero, false, false);
					EnemySprite.SendMessage("Animate", false);
					BroadcastMessage("Moving", false);
				}
			}else{
				EnemySprite.SendMessage("Animate", false);
			}
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
