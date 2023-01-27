using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static int id;
	public TMP_InputField inputField;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);    
    }

	public void SaveId() 
	{
		id = int.Parse(inputField.text);
		Debug.Log(id);
	}
}
