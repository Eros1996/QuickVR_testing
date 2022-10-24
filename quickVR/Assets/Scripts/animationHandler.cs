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

	private void animationEnd(string animationName)
	{
		animator.SetBool(animationName, false);
		QuickStageLearnTask.animationStart = false;
		QuickStageLearnTask.animationEnd = true;
	}

	private void animationStart()
	{

		QuickStageLearnTask.animationStart = true;
	}

}
