using UnityEngine;
using System.Collections;

public class GoalBoarder : MonoBehaviour {

    public CollectionBehavior.Key Key;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        print("Goal Collide");
        if(c.gameObject.tag == "Player")
        {
            print("Goal collide with player");
            var go = c.gameObject.GetComponent<Player2AxisMovement>(); 
            if(go.key == this.Key)
            {
                print("Goal is key");
                go.ResetKey();

                var r = Random.Range(0, 4);
                switch (r)
                {
                    case 0:
                        go.NotificationText.text = "MY POWER!!!!!! find more keys minion";
                        break;
                    case 1:
                        go.NotificationText.text = "As is sit here i wonder, where might I find more keys?";
                        break;
                    case 2:
                        go.NotificationText.text = "no keys, no gold, no fun";
                        break;
                    case 3:
                        go.NotificationText.text = "";
                        break;
                }

                Destroy(this.gameObject);
            }
        }
    }
}
