using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStagePerformTask : QuickStageBase
{
	protected override void Update()
	{
		if (InputManager.GetButtonDown("Continue"))
			endStage();
	}

	private void endStage()
	{
		this.Finish();
	}

	private void collectData() // positions
	{

	}
}
