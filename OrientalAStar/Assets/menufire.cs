﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menufire : StateMachineBehaviour {
	shoot ST;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	 override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		ST = animator.gameObject.GetComponent<shoot> ();

		float time=0.0f;
		RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller

		for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
		{
			if(ac.animationClips[i].name == "Shoot")        //If it has the same name as your clip
			{
				time = ac.animationClips[i].length;
			}
		}

	//	ST.ShootWithDelay(time/2);
		ST.Fire_Bullet();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}