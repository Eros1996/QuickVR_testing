using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStagePerforming : QuickStageBase
{
	public RecordAnimation GUI;

	public override void Init()
	{
		base.Init();
		ShowGUI(true);
	}

	protected override void Update()
	{
		base.Update();
	}

	public void GoToNextStage() 
	{ 
		this.Finish();
	}

	private void ShowGUI(bool show)
	{
		GUI.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
