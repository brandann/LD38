using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public enum scenes { Main = 0, PlayerInChangingWorld = 1, ScaleSquares = 2}
	#region ButtonPress
	public void PlayButtonPressed()
    {
        SceneManager.LoadScene("MoonEatMoon");
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