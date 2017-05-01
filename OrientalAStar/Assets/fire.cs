using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : StateMachineBehaviour {

	shoot ST;
	CharacterControllerLogic_T CCT;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	 override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	 {

		float time=0.0f;
		RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller
	
		for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
		{
			if(ac.animationClips[i].name == "Shoot")        //If it has the same name as your clip
			{
				time = ac.animationClips[i].length;
			}
		}
		CCT  = animator.gameObject.GetComponent<CharacterControllerLogic_T> ();
		ST = animator.gameObject.GetComponent<shoot> ();
		animator.SetBool ("fire", false);
		ST.ShootWithDelay(time/2);


     }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	 override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		CCT.fire_flag = true;
	 }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
