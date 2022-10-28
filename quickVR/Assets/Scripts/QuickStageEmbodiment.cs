using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStageEmbodiment : QuickStageBase
{
	public Animator animator;

	protected override void Update()
	{
		if (InputManager.GetButtonDown("Continue"))
		{
			this.Finish();
		}
	}
}
