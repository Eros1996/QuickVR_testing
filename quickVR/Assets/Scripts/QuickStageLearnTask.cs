using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStageLearnTask : QuickStageBase
{
	public Animator animator;
	public string animationName;

	protected override void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			endStage();

		if (Input.GetKeyDown(KeyCode.A))
		{
			animator.SetBool(animationName, true);
		}
	}

	private void endStage()
	{
		Debug.Log("LEARN TASK END");
		this.Finish();
	}
}
