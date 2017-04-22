using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	#region ButtonPress
	public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
    #endregion

    void Update()
    {
        if (Input.GetButtonDown(InputMap.GlobalOK))
        {
			PlayButtonPressed();
        }
    }
}