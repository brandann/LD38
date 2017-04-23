using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {

    public delegate void LevelupHandler(int level);
    public event LevelupHandler OnLevelup = delegate { };
    public static float WinWaitTimeToRestart = 3;

    private int _currentLevel = 1;
    public const int MAX_LEVEL = 4;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        private set { _currentLevel = value; }
    }

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
            StartCoroutine("WinRoutine");
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
