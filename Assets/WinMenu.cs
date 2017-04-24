using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {

    public enum scenes { Main = 0, PlayerInChangingWorld = 1, ScaleSquares = 2 }
    public Text text;
    #region ButtonPress
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Main");
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

        text.text = "<color=yellow><b>m o o n - e a t - m o o n</b></color>\n\nyou win\n\n<color=yellow><b>H i g h s c o r e :  " + CameraManager.FinalScore + "</b></color>\n\n< press enter/start >";

    }
}
