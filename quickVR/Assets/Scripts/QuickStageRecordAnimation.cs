using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You can now try to replicate the movement.
// Press the Record Performance button to record your movement.
// Whenever you feel okey with your performance press the Stop Recording button.

public class QuickStageRecordAnimation : QuickStageBase
{
	public RecordAnimation GUI;
	public QuickAnimationPlayer _animationPlayerSrc = null;
	public Animator animator;
	protected List<float[]> pos_rot_performance = new List<float[]>();
	private int col = 0;

	public virtual List<float[]> GetM1()
	{
		return pos_rot_performance;
	}

	public override void Init()
	{
		base.Init();
		ShowGUI(true);
	}

	protected override void Update()
	{
		base.Update();

		//if (!GUI.gameObject.activeSelf) 
		//{
		//	ShowGUI(false);
		//}

		if (_animationPlayerSrc.IsRecording()) 
		{
			SaveToMatrix();		
		}
	}

	public void GoToNextStage()
	{
		this.Finish();
	}

	public void SaveToMatrix() 
	{
		float[] temp = new float[6 * 24];
		col = 0;
		getBonePosition(animator.transform, temp);
		pos_rot_performance.Add(temp);
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

	public void ShowGUI(bool show)
	{
		GUI.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
