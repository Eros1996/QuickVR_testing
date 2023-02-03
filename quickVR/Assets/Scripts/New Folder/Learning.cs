using QuickVR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Learning : QuickStageBase
{
	public GameObject embodimentCanvas;
	public GameObject learningCanvas;
	public Animator Instructor;
	public QuickStageLoop _loop = null;
	public QuickUIButton _buttonLearnMovement = null;

	public static bool animationEnd;

	public override void Init()
	{
		base.Init();

		if (_vrManager.GetAnimatorSource().gameObject.TryGetComponent(out AsynchMovement asynchMovement))
		{
			var targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
			var meshRenderers = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
			foreach (var mesh in meshRenderers)
			{
				mesh.gameObject.SetActive(true);
			}

			asynchMovement.enabled = false;
		}
		embodimentCanvas.SetActive(false);
		_buttonLearnMovement.OnDown += ButtonLearnMovement_Down;
		learningCanvas.SetActive(true);
		ShowGUI(true);
	}

	protected override void Update()
	{
		base.Update();

		if (animationEnd) 
		{ 
			ShowGUI(false);
			this.Finish();
		}
	}

	private void ButtonLearnMovement_Down()
	{
		Instructor.SetBool("tai_chi_01", true);
		animationEnd = false;
		ShowGUI(false);
		UpdateStateButtonLearning();
	}

	private void ShowGUI(bool show)
	{
		_buttonLearnMovement.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}

	private void UpdateStateButtonLearning()
	{
		if (_buttonLearnMovement.GetComponentInChildren<TextMeshProUGUI>().text == "Learn Movement 3")
		{
			_buttonLearnMovement.GetComponentInChildren<TextMeshProUGUI>().text = " ";
		}
		else
		{
			int num = _loop.GetCurrentInteration() + 2;
			_buttonLearnMovement.GetComponentInChildren<TextMeshProUGUI>().text = "Learn Movement " + num;
		}
	}
}
