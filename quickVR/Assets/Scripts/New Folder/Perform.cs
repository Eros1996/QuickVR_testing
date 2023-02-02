using Mono.Reflection;
using QuickVR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perform : QuickStageBase
{
	public Animator user;
	public QuickStageLoop _loop = null;
	public QuickUIButton _buttonStartPerformance = null;

	public override void Init()
	{
		base.Init();

		_buttonStartPerformance.OnDown += ButtonStartPerformance_Down;
		ShowGUI(true);
	}



	protected override void Update()
	{
		base.Update();

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
		{
			user.SetBool("tai_chi_01", false);
			user.StopPlayback();
			this.Finish();
		}
	}

	private void ButtonStartPerformance_Down()
	{
		user.SetBool("tai_chi_01", true);
		ShowGUI(false);
		//UpdateStateButtonStartPerformance();
	}

	public void ShowGUI(bool show)
	{
		_buttonStartPerformance.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
