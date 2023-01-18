using QuickVR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordAnimation : MonoBehaviour
{
	public int id;
	public QuickAnimationPlayer _animationPlayerSrc = null;
	public QuickStageRecordAnimation _RecordAnimationStage = null;
	public QuickStageLearning _learningStage = null;
	public QuickStageLoop _loop = null;

	[Header("Animation UI")]
	public QuickUIButton _buttonLearnMovement = null;
	public QuickUIButton _buttonPerformMovement = null;

	public Animator learningAnimator;

	protected virtual void Awake()
	{
		_buttonLearnMovement.OnDown += ButtonLearnMovement_Down;
		_buttonPerformMovement.OnDown += ButtonPerformMovement_Down;
	}

	private void ButtonLearnMovement_Down()
	{
		learningAnimator.SetBool("tai_chi_01", true);
		_learningStage.ShowGUI(false);
		gameObject.SetActive(false);
	}

	private void ButtonPerformMovement_Down()
	{
		if (_animationPlayerSrc.IsRecording())
		{
			_animationPlayerSrc.StopRecording();
			string AnimationFileName;
			if (SceneManager.GetActiveScene().name == "RecordReferenceAnimation")
			{
				AnimationFileName = Application.dataPath + @"/../../../OutputData/ReferenceAnimation";
			}
			else
			{
				AnimationFileName = Application.dataPath + @"/../../../OutputData/" + SceneManager.GetActiveScene().name + "/subject" + id + "/PerformanceAnimation" + _loop.GetCurrentInteration();
			}

			QuickAnimationUtils.SaveToAnim(AnimationFileName + ".anim", _animationPlayerSrc.GetRecordedAnimation());
			SaveToFile(AnimationFileName);
			_RecordAnimationStage.GoToNextStage();
			Debug.Log("Recording Complete");
		}
		else
		{
			if (SceneManager.GetActiveScene().name == "RecordReferenceAnimation")
			{
				learningAnimator.SetBool("tai_chi_01", true);
			}
			_animationPlayerSrc.Record();
		}

		UpdateStateButtonRecordStop();
	}

	private void SaveToFile(string AnimationFile)
	{
		var fout = new StreamWriter(AnimationFile + ".csv");
		getBoneHeader(_animationPlayerSrc.transform, fout);
		fout.WriteLine();
		var m = _RecordAnimationStage.GetM1();
		WriteToFile(m, fout);
		fout.Close();
	}

	private static void WriteToFile(List<float[]> m1, StreamWriter fout)
	{
		for (int i = 0; i < m1.Count; i++)
		{
			for (int j = 0; j < 24 * 6; j++)
			{
				fout.Write(m1[i][j].ToString("F4") + ", ");
			}

			fout.WriteLine();
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

		if (p.name.Contains("hand")) return; // Do not write fingers header

		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (!child.name.Contains("__") && !child.name.Contains("_IK") && !child.name.Contains("Mesh") && !p.name.Contains("Body") && !p.name.Contains("Hair"))
				getBoneHeader(child, f);
		}
	}

	protected virtual void UpdateStateButtonRecordStop()
	{
		_buttonPerformMovement.GetComponentInChildren<TextMeshProUGUI>().text = (_animationPlayerSrc.IsRecording()) ? "Stop Recording" : "Record Performance";
	}
}
