using QuickVR;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordAnimation : MonoBehaviour
{
	public int id;
	public QuickAnimationPlayer _animationPlayerSrc = null;
	public QuickStagePerforming _performingStage = null;
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
		gameObject.SetActive(false);
	}

	private void ButtonPerformMovement_Down()
	{
		if (_animationPlayerSrc.IsRecording())
		{
			_animationPlayerSrc.StopRecording();
			QuickAnimationUtils.SaveToAnim("performance_" + _loop.GetCurrentInteration() + ".anim", _animationPlayerSrc.GetRecordedAnimation());
			_performingStage.GoToNextStage();
			Debug.Log("Recording Complete");
		}
		else
		{
			_animationPlayerSrc.Record();
		}

		UpdateStateButtonRecordStop();
	}

	protected virtual void UpdateStateButtonRecordStop()
	{
		_buttonPerformMovement.GetComponentInChildren<TextMeshProUGUI>().text = (_animationPlayerSrc.IsRecording()) ? "Stop Recording" : "Record Performance";
	}
}
