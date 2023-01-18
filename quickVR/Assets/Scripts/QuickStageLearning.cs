using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You can now learn a tai chi movement. It will be repeated consecutively 4 times.
// Press the learn movement button on your left to start it.
// Remember to stay still while looking at yourself/instructor and at the mirror to learn the movement.

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

		//if (!GUI.gameObject.activeSelf)
		//{
		//	ShowGUI(false);
		//}

		if (animationEnd)
		{
			animationEnd = false;
			this.Finish();
		}
	}

	public void ShowGUI(bool show) 
	{
		GUI.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
