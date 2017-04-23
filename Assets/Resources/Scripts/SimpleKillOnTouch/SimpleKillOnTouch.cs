using UnityEngine;
using System.Collections;

public class SimpleKillOnTouch : MonoBehaviour {

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

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