using QuickVR;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuickStageSaveToMatrix : QuickStageBase
{
	public QuickStageLoop quickStageLoop;
	public Animator referenceAnimator;
	public Animator animator;
	public static bool startPerformance = false;
	protected List<float[]> pos_rot_performance_1 = new List<float[]>();
	public static List<float[]> pos_rot_performance_2 = new List<float[]>();
	public static List<float[]> pos_rot_performance_3 = new List<float[]>();

	private int row = 0;
	private int col = 0;

	public virtual List<float[]> GetM1()
	{
		return pos_rot_performance_1;
	}

	public virtual List<float[]> GetM2()
	{
		return pos_rot_performance_2;
	}

	public virtual List<float[]> GetM3()
	{
		return pos_rot_performance_3;
	}

	public override void Init()
	{
		base.Init();
		
		row = 0;
	}

	protected override void Update()
	{
		base.Update();

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
		{
			startPerformance = false;
		}

		if (startPerformance)
		{
			float[] temp = new float[6*24];
			if (quickStageLoop.GetCurrentInteration() == 0)
			{
				col = 0;
				getBonePosition(animator.transform, temp);
				pos_rot_performance_1.Add(temp);
				row++;
			}
			else if (quickStageLoop.GetCurrentInteration() == 1)
			{
				col = 0;
				getBonePosition(animator.transform, temp);
				pos_rot_performance_2.Add(temp);
				row++;
			}
			else if (quickStageLoop.GetCurrentInteration() == 2)
			{
				col = 0;
				getBonePosition(animator.transform, temp);
				pos_rot_performance_3.Add(temp);
				row++;

			}
		}
	}

	private void getBonePosition(Transform p, float[] matrix)
	{
		if (p.name.Contains("__") || p.name.Contains("_IK") || p.name.Contains("Mesh") || p.name.Contains("Body") || p.name.Contains("Hair")) return;

		matrix[0 + 6 * col] = p.position.x;
		matrix[1 + 6 * col] = p.position.y;
		matrix[2 + 6 * col] = p.position.z;
		matrix[3 + 6 * col] = p.eulerAngles.x; 
		matrix[4 + 6 * col] = p.eulerAngles.y;
		matrix[5 + 6 * col] = p.eulerAngles.z;

		if (p.name.Contains("hand")) return; // Do not write fingers pos and rot

		for (int i = 0; i < p.childCount; i++)
		{
			var child = p.GetChild(i);
			if (!child.name.Contains("__") && !child.name.Contains("_IK") && !child.name.Contains("Mesh") && !p.name.Contains("Body") && !p.name.Contains("Hair"))
			{
				col++;
				getBonePosition(child, matrix);
			}
		}
	}

}
