using QuickVR;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Embodiment : QuickStageBase
{
	public GameObject embodimentCanvas;
    public List<GameObject> exerciseInstruction;
	public Vector3 moveBack = new Vector3 (0, 0, 0);

    private float _timeToChangeExercise;
	private float _currentTime;
	private int _currentExerciseIndex;

	public override void Init()
	{
		base.Init();

		embodimentCanvas.SetActive(true);

		_currentExerciseIndex = 0;
		_currentTime = 0;
		exerciseInstruction[_currentExerciseIndex].SetActive(true);
		_timeToChangeExercise = _maxTimeOut / exerciseInstruction.Count;

		if (_vrManager.GetAnimatorSource().gameObject.TryGetComponent(out AsynchMovement _asynchMovement))
		{
			//Debug.Log("Asynch");
			//var targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
			//var targetAvatar_ER = targetAvatar.AddComponent<QuickEnableRenderers>();
			//targetAvatar_ER._visible = false;

			var targetAvatar = _vrManager.GetAnimatorTarget().gameObject;
			var meshRenderers = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>();
			foreach (var mesh in meshRenderers)
			{
				mesh.gameObject.SetActive(false);
			}

			var AsynchPlayer = Instantiate(QuickStageChoosePlayer._selectedPlayer, targetAvatar.transform.position + moveBack, targetAvatar.transform.rotation);
			AsynchPlayer.AddComponent<QuickAnimationPlayer>();
			//var asynchPlayer_ER = AsynchPlayer.AddComponent<QuickEnableRenderers>();
			//asynchPlayer_ER._visible = true;

			targetAvatar.TryGetComponent(out QuickAnimationPlayer targetAnimationPlayer);
			if (targetAnimationPlayer == null)
				targetAnimationPlayer = targetAvatar.AddComponent<QuickAnimationPlayer>();

			_asynchMovement._playerMaster = targetAnimationPlayer;
			_asynchMovement._playerTarget = AsynchPlayer.GetComponent<QuickAnimationPlayer>();
			_asynchMovement.enabled = true;
		}
	}

	protected override void Update()
	{
		base.Update();

		_currentTime += Time.deltaTime;
		if (_currentTime >= _timeToChangeExercise && _currentExerciseIndex < exerciseInstruction.Count-1) 
		{
			Debug.Log(_currentExerciseIndex);
			_currentTime = 0;
			exerciseInstruction[_currentExerciseIndex].SetActive(false);
			_currentExerciseIndex++;
			exerciseInstruction[_currentExerciseIndex].SetActive(true);
		}
	}

	public void ShowGUI(bool show)
	{
		embodimentCanvas.SetActive(show);
		//_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
		_interactionManager._interactorHandRight.SetInteractorEnabled(InteractorType.UI, show);

	}
}
