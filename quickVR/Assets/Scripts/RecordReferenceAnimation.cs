using QuickVR;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordReferenceAnimation : MonoBehaviour
{
	public QuickAnimationPlayer _animationPlayerSrc = null;
	public QuickStageRecordAnimation _RecordReferenceAnimationStage = null;

	[Header("Animation UI")]
	public QuickUIButton _buttonPerformMovement = null;

	public Animator learningAnimator;

	protected virtual void Awake()
	{
		_buttonPerformMovement.OnDown += ButtonPerformMovement_Down;
	}

	private void ButtonPerformMovement_Down()
	{
		if (_animationPlayerSrc.IsRecording())
		{
			_animationPlayerSrc.StopRecording();
			var referenceAnimationFile = Application.dataPath + @"/../../../OutputData/ReferenceAnimation";

			QuickAnimationUtils.SaveToAnim(referenceAnimationFile + ".anim", _animationPlayerSrc.GetRecordedAnimation());
			SaveToFile(referenceAnimationFile);
			_RecordReferenceAnimationStage.GoToNextStage();
			Debug.Log("Recording Complete");
		}
		else
		{
			learningAnimator.SetBool("tai_chi_01", true);
			_animationPlayerSrc.Record();
		}

		UpdateStateButtonRecordStop();
	}

	private void SaveToFile(string referenceAnimationFile) 
	{
		var fout = new StreamWriter(referenceAnimationFile + ".csv");
		getBoneHeader(learningAnimator.transform, fout);
		fout.WriteLine();
		var m = _RecordReferenceAnimationStage.GetM1();
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
