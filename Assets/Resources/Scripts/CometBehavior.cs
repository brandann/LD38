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
        if (c.gameObject.tag.Contains("Player"))
        {

            var thisscale = this.gameObject.transform.localScale.x;
            var otherscale = c.gameObject.transform.localScale.x;

            if (thisscale > otherscale)
            {
                print("kill player");
                c.gameObject.SendMessage("kill");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Contains("Player"))
        {
            var thisscale = this.gameObject.transform.localScale.x;
            var otherscale = c.gameObject.transform.localScale.x;

            if (thisscale > otherscale)
            {
                print("kill player with trigger");
                c.SendMessage("kill");
            }
            else
            {
                print("Absorbed to player");
                c.gameObject.SendMessage("absorb", this.transform.localScale.x);
                Destroy(this.gameObject);
            }
        }
    }
}
