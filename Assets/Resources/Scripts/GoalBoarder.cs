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
            if(c.gameObject.GetComponent<Player2AxisMovement>().key == this.Key)
            {
                print("Goal is key");
                Destroy(this.gameObject);
            }
        }
    }
}
