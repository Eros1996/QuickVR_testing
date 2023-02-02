using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embodiment : QuickStageBase
{
	public GameObject embodimentCanvas;
    public List<GameObject> exerciseInstruction;

    private float _timeToChangeExercise;
	private float _currentTime;
	private int _currentExerciseIndex;

	public override void Init()
	{
		base.Init();

		embodimentCanvas.SetActive(true);

		_currentExerciseIndex = 0;
		exerciseInstruction[_currentExerciseIndex].SetActive(true);
		_timeToChangeExercise = _maxTimeOut / exerciseInstruction.Count;
	}

	protected override void Update()
	{
		base.Update();

		_currentTime += Time.deltaTime;
		if (_currentTime >= _timeToChangeExercise && _currentExerciseIndex < exerciseInstruction.Count) 
		{
			_currentTime = 0;
			exerciseInstruction[_currentExerciseIndex].SetActive(false);
			_currentExerciseIndex++;
			exerciseInstruction[_currentExerciseIndex].SetActive(true);
		}
	}

	public void ShowGUI(bool show)
	{
		embodimentCanvas.SetActive(show);
		_interactionManager.GetVRInteractorHandRight().SetInteractorEnabled(InteractorType.UI, show);
	}
}
