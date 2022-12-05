using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

// Welcome to your first Tai Chi lesson. 
// Before start, look at the mirror and move your body, your arms and yor legs.
// Now look down to your body and feel it while moving it.

public class QuickStageEmbodiment : QuickStageBase
{

	//protected override IEnumerator CoUpdate()
	//{
	//	while (!InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
	//	{
	//		yield return null;
	//	}
	//}

	protected override void Update()
	{
		base.Update();

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
		{
			this.Finish();
		}
	}
}
