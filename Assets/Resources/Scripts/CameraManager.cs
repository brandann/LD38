using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
