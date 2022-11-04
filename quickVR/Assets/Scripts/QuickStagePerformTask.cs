using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;
using UnityEngine.SceneManagement;

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
	string[] animationIndex = { "01", "06", "12" };
	string animationName = "tai_chi_";

	#endregion

	public override void Init()
	{
		animator = _vrManager.GetAnimatorTarget();
		_unityVR = animator.GetComponent<QuickUnityVR>();

		_performanceFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject0/performance" + quickStageLoop.GetCurrentInteration() + ".csv";
		_refAnimationFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject0/refAnimation" + quickStageLoop.GetCurrentInteration() + ".csv";

		startPerformance = false;
		headerWritten = false;
		base.Init();
	}

	protected override void Update()
	{
		if (InputManager.GetButtonDown("Continue"))
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
				getBonesHeader(animator.transform.GetChild(0), fout);
				getBonesHeader(referenceAnimator.transform.GetChild(0), fout1);

				headerWritten = true;
			}

			fout.WriteLine();
			fout1.WriteLine();

			getBonesPosition(animator.transform.GetChild(0), fout);
			getBonesPosition(referenceAnimator.transform.GetChild(0), fout1);
		}
	}

	private void getBonesHeader(Transform p, StreamWriter f)
	{
		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (child.name.Contains("B-") || (child.name.Contains("Bip") && !child.name.Contains("Footsteps")))
			{
				f.Write(child.name + "-X, ");
				f.Write(child.name + "-Y, ");
				f.Write(child.name + "-Z, ");

				getBonesHeader(child, f);
			}
		}
	}

	private void getBonesPosition(Transform p, StreamWriter f)
	{
		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (child.name.Contains("B-") || (child.name.Contains("Bip") && !child.name.Contains("Footsteps")))
			{
				f.Write(child.localPosition.ToString("F4").Replace("(", "").Replace(")", "") + ", ");

				getBonesPosition(child, f);
			}
		}
	}
}
