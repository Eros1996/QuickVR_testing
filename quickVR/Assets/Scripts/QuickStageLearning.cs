using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You can now learn a tai chi movement. It will be repeated consecutively 4 times.
// Press the learn movement button on your left to start it.

public class QuickStageLearning : QuickStageBase
{
	public static bool animationEnd;
	public RecordAnimation GUI;

	private GameObject goPlayer;

	public override void Init()
	{
		base.Init();
		animationEnd = false;
		ShowGUI(true);
	}

	protected override void Update()
	{
		base.Update();

		if (animationEnd)
		{
			EndAnimation();
			animationEnd = false;
			this.Finish();
		}
	}

	public void ShowGUI(bool show) 
	{
		GUI.gameObject.SetActive(show);
		//_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
		_interactionManager._interactorHandRight.SetInteractorEnabled(InteractorType.UI, show);

	}

	public void StartAnimation() 
	{
		var targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
		var animatorController = _vrManager.GetAnimatorSource().runtimeAnimatorController;

		var meshRenderers = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (var mesh in meshRenderers)
		{
			mesh.gameObject.SetActive(false);
		}

		goPlayer = Instantiate(QuickStageChoosePlayer._selectedPlayer);
		goPlayer.transform.position = targetAvatar.transform.position;
		goPlayer.transform.rotation = targetAvatar.transform.rotation;

		var anim = goPlayer.GetComponent<Animator>();
		anim.runtimeAnimatorController = animatorController;
		anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		anim.applyRootMotion = false;
		anim.SetBool("tai_chi_01", true);

	}

	public void EndAnimation() 
	{
		var targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
		var meshRenderers = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		foreach (var mesh in meshRenderers)
		{
			mesh.gameObject.SetActive(true);
		}

		Destroy(goPlayer);
	}
}
