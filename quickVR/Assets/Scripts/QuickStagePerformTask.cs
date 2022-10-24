using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;
using System.IO;

public class QuickStagePerformTask : QuickStageBase
{
	#region PUBLIC ATTRIBUTES

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
	bool startPerformance = false;

	#endregion

	public override void Init()
	{
		animator = _vrManager.GetAnimatorTarget();
		_unityVR = animator.GetComponent<QuickUnityVR>();

		_animationFile = Application.dataPath + @"/../../../OutputData/movement" + quickStageLoop.GetCurrentInteration() + ".csv";

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
			this.Finish();
		}

		if (InputManager.GetButtonDown("StartAnimation"))
		{
			startPerformance = true;
			fout = new StreamWriter(_animationFile);
		}

		if (startPerformance)
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
