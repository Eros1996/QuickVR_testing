using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuickVR;

public class animationHandler : MonoBehaviour
{
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void animationEnd(string animationName)
	{
		QuickStageLearnTask.animationStart = false;

		animator.SetBool(animationName, false);
		var unityVR = GetComponent<QuickUnityVR>();
		unityVR.SetIKControl(IKBone.Hips, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftHand, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightHand, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftFoot, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightFoot, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftIndexDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftLittleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftMiddleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftRingDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.LeftThumbDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightIndexDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightLittleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightMiddleDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightRingDistal, QuickUnityVR.ControlType.Tracking);
		unityVR.SetIKControl(IKBone.RightThumbDistal, QuickUnityVR.ControlType.Tracking);

		QuickStageLearnTask.animationEnd = true;
	}
}
