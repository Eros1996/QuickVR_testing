using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

// Welcome to your first tai Chi lesson. 
// Before start, look at the mirror and move your body, your arms and your legs.
// Now look down to your body and feel it while moving it. 
// Whenever you are ready, press the right trigger to start learning tai chi.

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
