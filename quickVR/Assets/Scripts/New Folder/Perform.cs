using Mono.Reflection;
using QuickVR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Perform : QuickStageBase
{
	public QuickUIButton _buttonStartPerformance = null;
	public static bool animationEnd;

	private GameObject animatedPlayer;
	private GameObject targetAvatar;
	private GameObject sourceAvatar;

	public override void Init()
	{
		base.Init();

		_buttonStartPerformance.OnDown += ButtonStartPerformance_Down;
		targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
		sourceAvatar = _vrManager.GetAnimatorSource().gameObject;
		animationEnd = false;
		animatedPlayer = null;
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
		var animatorController = sourceAvatar.GetComponent<Animator>().runtimeAnimatorController;

		var meshRenderers = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (var mesh in meshRenderers)
		{
			mesh.gameObject.SetActive(false);
		}

		if (animatedPlayer == null)
		{
			animatedPlayer = Instantiate(QuickStageChoosePlayer._selectedPlayer, targetAvatar.transform.position, targetAvatar.transform.rotation);
		}

		var anim = animatedPlayer.GetComponent<Animator>();
		anim.runtimeAnimatorController = animatorController;
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

		DestroyImmediate(animatedPlayer);
		animatedPlayer = null;

	}
}
