using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStagePerformTask : QuickStageBase
{
	protected override void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			endStage();
	}

	private void endStage()
	{
		Debug.Log("PERFORM TASK END");
		this.Finish();
	}
}
