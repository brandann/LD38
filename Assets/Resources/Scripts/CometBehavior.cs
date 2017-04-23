using UnityEngine;
using System.Collections;

public class CometBehavior : MonoBehaviour {

    private float starttime;
    public float Duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((Time.timeSinceLevelLoad - starttime) > Duration)
        {
            Destroy(this);
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Contains("Player"))
        {
            c.SendMessage("comethit");
        }
    }
}
