using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;
using UnityEngine.SceneManagement;

// You are now at the PERFORM stage
// Press the Left Trigger to when you decide to perform the Tai Chi animation
// Press the Right Trigger to go to the next stage

public class QuickStagePerformTask : QuickStageBase
{
	#region PUBLIC ATTRIBUTES

	public QuickStageLoop quickStageLoop;
	public Animator referenceAnimator;
	public static bool startPerformance = false;
	
	#endregion

	#region PROTECTED ATTRIBUTES

	protected Animator animator;
	protected QuickUnityVR _unityVR = null;

	#endregion

	#region PRIVATE ATTRIBUTES

	bool headerWritten = false;
	string _performanceFile, _refAnimationFile;
	StreamWriter fout, fout1;
	string[] animationIndex = { "01", "01", "01" };
	string animationName = "tai_chi_";

	#endregion

	public override void Init()
	{
		base.Init();

		animator = _vrManager.GetAnimatorTarget();
		_unityVR = animator.GetComponent<QuickUnityVR>();

		_performanceFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject0/performance" + quickStageLoop.GetCurrentInteration() + ".csv";
		_refAnimationFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject0/refAnimation" + quickStageLoop.GetCurrentInteration() + ".csv";

		startPerformance = false;
		headerWritten = false;
	}

	protected override void Update()
	{
		base.Update();

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
		{
			startPerformance = false;
			fout.Close();
			fout1.Close();

			this.Finish();
		}

		if (InputManager.GetButtonDown("StartAnimation"))
		{
			startPerformance = true;
			referenceAnimator.SetBool(animationName + animationIndex[quickStageLoop.GetCurrentInteration()], true);

			fout = new StreamWriter(_performanceFile);
			fout1 = new StreamWriter(_refAnimationFile);
		}

		if (startPerformance)
		{
			if (!headerWritten)
			{
				getBoneHeader(animator.transform, fout);
				getBoneHeader(referenceAnimator.transform, fout1);

				headerWritten = true;
			}

			fout.WriteLine();
			fout1.WriteLine();

			getBonePosition(animator.transform, fout);
			getBonePosition(referenceAnimator.transform, fout1);
		}
	}

	private void getBoneHeader(Transform p, StreamWriter f)
	{
		if (p.name.Contains("__") || p.name.Contains("_IK") || p.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair")) return;
		
		f.Write(p.name + "-posX, ");
		f.Write(p.name + "-posY, ");
		f.Write(p.name + "-posZ, ");

		f.Write(p.name + "-rotX, ");
		f.Write(p.name + "-rotY, ");
		f.Write(p.name + "-rotZ, ");

		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (!child.name.Contains("__") && !child.name.Contains("_IK") && !child.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair"))
				getBoneHeader(child, f);
		}
		
	}

	private void getBonePosition(Transform p, StreamWriter f)
	{
		if (p.name.Contains("__") || p.name.Contains("_IK") || p.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair")) return;

		f.Write(p.position.ToString("F4").Replace("(", "").Replace(")", "") + ", ");
		f.Write(p.rotation.ToString("F4").Replace("(", "").Replace(")", "") + ", ");

		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (!child.name.Contains("__") && !child.name.Contains("_IK") && !child.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair"))
				getBonePosition(child, f);
		}
	}
}
