using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public enum scenes { Main = 0, PlayerInChangingWorld = 1, ScaleSquares = 2}
    public Text text;
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

        text.text = "<color=yellow><b>m o o n - e a t - m o o n</b></color>\n\nLudum Dare #38\nBrandan Haertel\nGary Mixson\n\n<color=yellow><b>H i g h s c o r e :  " + CameraManager.FinalScore + "</b></color>\n\n< press enter/start >";
       

        //if(CameraManager.FinalScore > 0)
        //{
        //    text.text = "<color=yellow><b>m o o n - e a t - m o o n</b></color>\n\nLudum Dare #38\nBrandan Haertel\nGary Mixson\n\n<color=yellow><b>Highscore: " + CameraManager.FinalScore + "</b></color>\n\n< press enter >\n\nMusic By RutgerMuller\n(Freesounds.org)";
        //}
        //else
        //{
        //    text.text = "<color=yellow><b>m o o n - e a t - m o o n</b></color>\n\nLudum Dare #38\nBrandan Haertel\nGary Mixson\n\n< press enter >\n\nMusic By RutgerMuller\n(Freesounds.org)";
        //}
    }
}