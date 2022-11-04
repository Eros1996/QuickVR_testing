using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfLearningInit_SM : StateMachineBehaviour
{
	string name;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var unityVR = animator.GetComponent<QuickUnityVR>();
		unityVR.SetIKControl(IKBone.Head, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.Hips, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftHand, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightHand, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftFoot, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightFoot, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftIndexDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftLittleDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftMiddleDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftRingDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.LeftThumbDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightIndexDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightLittleDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightMiddleDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightRingDistal, QuickUnityVR.ControlType.Animation);
		unityVR.SetIKControl(IKBone.RightThumbDistal, QuickUnityVR.ControlType.Animation);

		QuickStageLearnTask.animationStart = true;
		name = animator.GetNextAnimatorClipInfo(layerIndex)[0].clip.name;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//Debug.Log(animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name);
		animator.SetBool(name, false);
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
