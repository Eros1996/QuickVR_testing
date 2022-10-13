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
		animator.SetBool(animationName, false);
	}
}
