using UnityEngine;
using System.Collections;

public class Player2AxisMovement : MonoBehaviour
{
    private float _speed = 12;
    private Vector3 mStartingPosition;
    private Vector3 mVelocity;

    void Start()
    {
        mVelocity = new Vector3(0, 0, 0);
        mStartingPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		// GET THE VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
		if (Input.GetKey(KeyCode.UpArrow))
			mVelocity.y = 1;
		else if (Input.GetKey(KeyCode.DownArrow))
			mVelocity.y = -1;
		else
			mVelocity.y = 0;

		if (Input.GetKey(KeyCode.RightArrow))
			mVelocity.x = 1;
		else if (Input.GetKey(KeyCode.LeftArrow))
			mVelocity.x = -1;
		else
			mVelocity.x = 0;
        
        GetComponent<Rigidbody2D>().velocity = (mVelocity.normalized * mVelocity.magnitude * _speed);
    }

	public void scaleme(float s)
	{
		print("I got scaled: " + s);
		this._speed *= s;
	}

	public GameObject burstPrefab;

	public void kill()
	{
		var bgo = Instantiate(burstPrefab);
		bgo.transform.position = this.transform.position;
		var bm = bgo.GetComponent<BurstManager>();
		bm.MakeBurst(30, Color.white, this.transform.position, this.transform.localScale.x);

		Destroy(this.gameObject);

	}
}
