using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;

public class QuickStageLearnTask : QuickStageBase
{

	#region PUBLIC ATTRIBUTES

	public string animationName;
	public static bool animationEnd = false;
	public static bool animationStart = false;
	public QuickStageLoop quickStageLoop;

	#endregion

	#region PROTECTED ATTRIBUTES

	protected Animator animator;
	protected QuickUnityVR _unityVR = null;

	#endregion

	#region PRIVATE ATTRIBUTES

	List<Vector3> avatarsArray = new List<Vector3>();
	List<string> header = new List<string>();
	bool headerWritten = false;
	string _animationFile;
	StreamWriter fout;
	int count = 0;
	string[] animationIndex = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "11", "12", "13" };

	#endregion

	public override void Init()
	{
		
		animator = _vrManager.GetAnimatorTarget();
		_unityVR = animator.GetComponent<QuickUnityVR>();
		
		_animationFile = Application.dataPath + @"/../../../OutputData/animation"+ quickStageLoop.GetCurrentInteration() + ".csv";

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
			SettingIKControl2Animation();
			animator.SetBool(animationName+animationIndex[quickStageLoop.GetCurrentInteration()], true);
			animationStart = true;

			fout = new StreamWriter(_animationFile);
		}

		if (animationStart)
		{
			avatarsArray.Clear();
			AvatarStructure(_unityVR.transform.GetChild(0));
			if (!headerWritten)
			{
				foreach (var item in header)
				{
					fout.Write(item);
				}

				headerWritten = true;
				header.Clear();
			}
			fout.WriteLine();

			foreach (var item in avatarsArray)
			{
				fout.Write(item.ToString("F4").Replace("(", "").Replace(")", "") + ", ");
			}
			count++;
		}
	}

	private void SettingIKControl2Animation()
	{
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
	}

	private void AvatarStructure(Transform p)
	{
		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (child.name.Contains("B-"))
			{
				if (!headerWritten)
				{
					header.Add(child.name + "X, ");
					header.Add(child.name + "Y, ");
					header.Add(child.name + "Z, ");
				}

				avatarsArray.Add(child.position);
				AvatarStructure(child);
			}
		}
	}
}
