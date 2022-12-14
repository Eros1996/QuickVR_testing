using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfLearningEnd_SM : StateMachineBehaviour
{
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetInteger("TaichiCounter", animator.GetInteger("TaichiCounter") + 1);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (animator.GetInteger("TaichiCounter") < 4) return;

		QuickStageLearnTask.animationEnd = true;
		QuickStagePerformTask.startPerformance = false;

		var unityVR = animator.GetComponent<QuickUnityVR>();
		unityVR.SetIKControl(IKBone.Hips, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftHand, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightHand, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftFoot, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightFoot, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftIndexDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftLittleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftMiddleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftRingDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftThumbDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightIndexDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightLittleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightMiddleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightRingDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightThumbDistal, QuickUnityVR.ControlType.Tracking);

		animator.SetInteger("TaichiCounter", 0);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
