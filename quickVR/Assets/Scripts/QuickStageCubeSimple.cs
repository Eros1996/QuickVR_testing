using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStageCubeSimple : QuickStageBase
{
	public Animator animator;
	protected override IEnumerator CoUpdate()
	{
		yield return new WaitForSeconds(2f);

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		_vrManager.SetAnimatorTarget(animator);
		
	}
}
