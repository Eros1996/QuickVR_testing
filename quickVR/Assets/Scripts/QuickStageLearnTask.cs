using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;
using UnityEngine.SceneManagement;

// You are now at the LEARN stage
// Press the Left Trigger to start the Tai Chi animation that you have to learn
// Press the Right Trigger to go to the next stage

public class QuickStageLearnTask : QuickStageBase
{
	#region PUBLIC ATTRIBUTES
	public static bool animationEnd;
	public QuickStageLoop quickStageLoop;
	public Animator animator;
	#endregion

	#region PROTECTED ATTRIBUTES

	//protected Animator animator;
	protected QuickUnityVR _unityVR = null;

	#endregion

	#region PRIVATE ATTRIBUTES

	string[] animationIndex = {"01", "01", "01"};
	string animationName = "tai_chi_";

	#endregion

	public override void Init()
	{
		base.Init();
		animationEnd = false;
	}

	//protected override IEnumerator CoUpdate()
	//{
	//	while (!InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
	//	{
	//		if (InputManager.GetButtonDown("StartAnimation"))
	//		{
	//			animator.SetBool(animationName + animationIndex[quickStageLoop.GetCurrentInteration()], true);
	//		}

	//		yield return null;
	//	}
	//}

	protected override void Update()
	{
		base.Update();

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE) && animationEnd)
		{
			this.Finish();
		}

		if (InputManager.GetButtonDown("StartAnimation"))
		{
			animator.SetBool(animationName + animationIndex[quickStageLoop.GetCurrentInteration()], true);
		}
	}
}
