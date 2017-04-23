using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {

    public delegate void LevelupHandler(int level);
    public event LevelupHandler OnLevelup = delegate { };

	// Use this for initialization
	void Start () {
        GameObject.Find("Player").GetComponent<Player2AxisMovement>().OnPlayerLevelup += this.PlayerLevelup;
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
        OnLevelup(-1);
    }
}
