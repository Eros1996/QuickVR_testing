using Mono.Reflection;
using QuickVR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Perform : QuickStageBase
{
	public QuickStageLoop _loop = null;
	public QuickUIButton _buttonStartPerformance = null;
	public static bool animationEnd;

	private GameObject goPlayer;

	public override void Init()
	{
		base.Init();

		_buttonStartPerformance.OnDown += ButtonStartPerformance_Down;
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

		if (InputManager.GetButtonDown(InputManager.DEFAULT_BUTTON_CONTINUE))
		{
			EndAnimation();
			animationEnd = false; 
			goPlayer.GetComponent<Animator>().StopPlayback();
			this.Finish();
		}
	}

	private void ButtonStartPerformance_Down()
	{
		ShowGUI(false);
		StartAnimation();
	}

	public void ShowGUI(bool show)
	{
		_buttonStartPerformance.gameObject.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
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
		anim.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
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
