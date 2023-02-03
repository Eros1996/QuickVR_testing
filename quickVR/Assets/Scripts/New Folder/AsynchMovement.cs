using QuickVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsynchMovement : MonoBehaviour
{
	public QuickAnimationPlayer _playerMaster = null;
	public QuickAnimationPlayer _playerTarget = null;

	public float _delay = 5.0f;

	public virtual IEnumerator Start()
	{
		_playerMaster.Record();

		yield return new WaitForSeconds(_delay);

		_playerTarget.Play(_playerMaster.GetRecordedAnimation());
	}

	private void OnDisable()
	{
		Destroy(_playerMaster);
		Destroy(_playerTarget.gameObject);
	}
}
