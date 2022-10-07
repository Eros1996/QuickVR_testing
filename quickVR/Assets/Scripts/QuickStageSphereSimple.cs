using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class QuickStageSphereSimple : QuickStageBase
{
	protected override IEnumerator CoUpdate()
	{
		yield return new WaitForSeconds(2f);

		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	}
}
