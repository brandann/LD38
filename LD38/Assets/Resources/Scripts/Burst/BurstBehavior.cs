using UnityEngine;
using System.Collections;

public class BurstBehavior : MonoBehaviour {

	public enum eBurstSprites {
		Square = 0,
		Circle = 1,
		Diamond = 2,
		Hex = 3,
		Star3 = 4,
		Star5 = 5,
		Heart = 6,
		Triangle = 7,

		Random = 99
	};

	// MIN, MAX OF SPEED APPLIED TO THE SINGLE
	// BURST GAMEOBJECTS
	public Vector2 RandomSpeedRange;
	
	// MIN, MAX OF THE DECAY RATE FOR THE SINGLE
	// BURST GAMEOBJECTS
	public Vector2 RandomDecayRate;
	
	// SET AS RANDOM OF RANGE RandomSpeedRange
    private float mSpeed;
    
	// SET AS RANDOM OF RANGE RandomDecayRate
	// FORCED TO BE A VAL OF .001 - .999
    private float mDecay;
    
    // MIN SIZE OF THE GAMEOBJECT BEFORE BEING DESTROYED
    private float MIN_SIZE = 0.1f;

	public Sprite[] BurstSprites;

    void Start ()
    {
        mSpeed = Random.Range (RandomSpeedRange[0], RandomSpeedRange[1]);
		mDecay = Random.Range (RandomDecayRate[0], RandomDecayRate[1]);
		mDecay = Mathf.Clamp(mDecay, .001f, .999f);
    }
    
    void Update ()
    {
    	// THE DEATH OF THIS OBJECT IS BASED OFF THE SIZE OF THE OBJECT.
    	// WHEN THE SCALE GETS TO MIN_SCALE THEN DESTROY
		if (this.transform.localScale.x < MIN_SIZE) {
			Destroy(this.gameObject);
      	}
      	
      	// MOVE THE GAMEOBJECT FORWARD BASED ON TRANSFORM.UP
      	// THE ROATION IS SET BY THE BURST MANAGER
      	transform.position += mSpeed * Time.smoothDeltaTime * transform.up;
      	
      	// SCALE THE GAMEOBJECT DOWN BASED ON THE DECAY RATE
      	this.transform.localScale *= mDecay;
    }
}
