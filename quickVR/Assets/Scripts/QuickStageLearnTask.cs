using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStageLearnTask : QuickStageBase
{

	#region PUBLIC ATTRIBUTES

	public string animationName;
	public static bool animationEnd = false;
	public static bool animationStart = false;

	#endregion

	#region PROTECTED ATTRIBUTES

	protected Animator animator;
	protected QuickUnityVR _unityVR = null;

	#endregion

	public override void Init()
	{
		animator = _vrManager.GetAnimatorTarget();
		_unityVR = animator.GetComponent<QuickUnityVR>();
		animationEnd = false;
		animationStart = false;
		base.Init();
	}

	protected override void Update()
	{
		if (InputManager.GetButtonDown("Continue") && animationEnd)
			endStage();

		if (InputManager.GetButtonDown("StartAnimation"))
		{
			animator.SetBool(animationName, true);
			_unityVR.SetIKControl(IKBone.Hips, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftHand, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightHand, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftFoot, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightFoot, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftIndexDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftLittleDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftMiddleDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftRingDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.LeftThumbDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightIndexDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightLittleDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightMiddleDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightRingDistal, QuickUnityVR.ControlType.Animation);
			_unityVR.SetIKControl(IKBone.RightThumbDistal, QuickUnityVR.ControlType.Animation);
			animationStart = true;
		}

		if (animationStart) // eye tracking analysis
		{
			lookingAt();
		}
	}

	private void endStage()
	{
		this.Finish();
	}

	private void lookingAt()
	{

	}
}
