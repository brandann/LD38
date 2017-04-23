using UnityEngine;
using System.Collections;

public class SlowComet : MonoBehaviour {

    private float starttime;
    public float Duration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.timeSinceLevelLoad - starttime) > Duration)
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Contains("Player"))
        {
            Time.timeScale = .3f;
            StartCoroutine("ResetRoutine");
        }
    }

    // COROUTINE FOR SPEED MOD
    IEnumerator ResetRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(3*.5f);
        Time.timeScale = 1;
        yield return null;
    }
}
