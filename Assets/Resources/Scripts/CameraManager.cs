using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

    public delegate void LevelupHandler(int level);
    public event LevelupHandler OnLevelup = delegate { };
    public static float WinWaitTimeToRestart = 3;

    public static int FinalScore;

    private int _currentLevel = 1;
    public const int MAX_LEVEL = 4;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        private set { _currentLevel = value; }
    }

    public Text NotificationText;

	// Use this for initialization
	void Start () {
        GameObject.Find("Player").GetComponent<Player2AxisMovement>().OnPlayerLevelup += this.PlayerLevelup;
        OnLevelup(CurrentLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void restartAfter3Seconds()
	{
		StartCoroutine("ResetRoutine");
	}

	// COROUTINE FOR SPEED MOD
	IEnumerator ResetRoutine()
	{
		// WAIT FOR THE MOD DURATION TO FINISH
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene((int)MainMenu.scenes.Main);
		yield return null;
	}

    private void PlayerLevelup()
    {
        print("player leveled up on global");
        CurrentLevel++;
        if (CurrentLevel <= MAX_LEVEL)
        {
            StartCoroutine("WinRoutine");

            var r = Random.Range(0, 5);
            switch (r)
            {
                case 0:
                    NotificationText.text = "i applaud your ability to level up!";
                    break;
                case 1:
                    NotificationText.text = "hooray! you successfully navigated to the next level!";
                    break;
                case 2:
                    NotificationText.text = "you skills show promise, care to demonstrate again?";
                    break;
                case 3:
                    NotificationText.text = "the squeels of tiny cheers can be heard, go on the the next round.";
                    break;
                case 4:
                    NotificationText.text = "you continue to impress your people, do not disappoint them.";
                    break;
            }
        }
        else
            SceneManager.LoadScene("Main");
    }

    // COROUTINE FOR SPEED MOD
    IEnumerator WinRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(WinWaitTimeToRestart);
        var l = GameObject.FindGameObjectsWithTag("SpawnedBlocks");
        foreach (GameObject p in l)
        {
            Destroy(p);
        }

        //Planet
        var planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject p in planets)
        {
            Destroy(p);
        }
        OnLevelup(_currentLevel);
        print("level: " + _currentLevel);
        yield return null;
    }
}
