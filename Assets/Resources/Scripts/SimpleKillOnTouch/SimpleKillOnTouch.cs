using UnityEngine;
using System.Collections;

public class SimpleKillOnTouch : MonoBehaviour {

    public GameObject burstPrefab;

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
        if (c.gameObject.tag.Contains("Player"))
        {
            print("kill player with trigger");
            c.SendMessage("kill");
            var go = Instantiate(burstPrefab);
            go.transform.position = this.transform.position;
            var burst = go.GetComponent<BurstManager>();
            burst.MakeBurst(10, Color.red, this.transform.position, this.transform.localScale.x);
            Destroy(this);
        }

	}
}