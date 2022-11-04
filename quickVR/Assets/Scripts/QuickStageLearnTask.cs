using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;
using UnityEngine.SceneManagement;

public class QuickStageLearnTask : QuickStageBase
{

	#region PUBLIC ATTRIBUTES

	public static bool animationEnd = false;
	public static bool animationStart = false;
	public QuickStageLoop quickStageLoop;
	public Animator animator;
	#endregion

	#region PROTECTED ATTRIBUTES

	//protected Animator animator;
	protected QuickUnityVR _unityVR = null;

	#endregion

	#region PRIVATE ATTRIBUTES

	bool headerWritten = false;
	string _animationFile;
	StreamWriter fout;
	//string[] animationIndex = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "11", "12", "13"};
	string[] animationIndex = {"01", "06", "12"};
	string animationName = "tai_chi_";

	#endregion

	public override void Init()
	{
		if (animator == null)
		{
			animator = _vrManager.GetAnimatorTarget();
			_unityVR = animator.GetComponent<QuickUnityVR>();
		}

		_animationFile = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject0/animation" + quickStageLoop.GetCurrentInteration() + ".csv";

		headerWritten = false;
		animationEnd = false;
		animationStart = false;

		base.Init();
	}

	protected override void Update()
	{
		if (InputManager.GetButtonDown("Continue") && animationEnd)
		{
			fout.Close();
			this.Finish();
		}

		if (InputManager.GetButtonDown("StartAnimation"))
		{
			animator.SetBool(animationName+animationIndex[quickStageLoop.GetCurrentInteration()], true);
			animationStart = true;

			fout = new StreamWriter(_animationFile);
		}

		if (animationStart)
		{
			if (!headerWritten)
			{
				getBonesHeader(animator.transform.GetChild(0));
				headerWritten = true;
			}

			fout.WriteLine();
			getBonesPosition(animator.transform.GetChild(0));
		}
	}

	private void getBonesHeader(Transform p)
	{
		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (child.name.Contains("B-") || (child.name.Contains("Bip") && !child.name.Contains("Footsteps")))
			{
				fout.Write(child.name + "-X, ");
				fout.Write(child.name + "-Y, ");
				fout.Write(child.name + "-Z, ");

				getBonesHeader(child);
			}
		}
	}

	private void getBonesPosition(Transform p)
	{
		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (child.name.Contains("B-") || (child.name.Contains("Bip") && !child.name.Contains("Footsteps")))
			{
				fout.Write(child.localPosition.ToString("F4").Replace("(", "").Replace(")", "") + ", ");

				getBonesPosition(child);
			}
		}
	}
}
