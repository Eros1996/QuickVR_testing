using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStageLearning : QuickStageBase
{
	public static bool animationEnd;
	public RecordAnimation GUI;

	public override void Init()
	{
		base.Init();
		animationEnd = false;
		ShowGUI(true);
	}

	protected override void Update()
	{
		base.Update();

		if (animationEnd)
		{
			animationEnd = false;
			this.Finish();
		}
	}

	private void ShowGUI(bool show) 
	{
		GUI.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
