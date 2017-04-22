using UnityEngine;
using System.Collections;

public class Schooler : MonoBehaviour {

	static Vector3 Goal;
	public GameObject Player;
	public float Speed = 1;
	private Vector2 _direction = new Vector2(0, 0).normalized;
	Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody2D>();
		PickRandomDirection();
	}
	
	// Update is called once per frame
	void Update () {
		if(Player != null)
		{
			Goal = Player.transform.position;
		}

		_direction = (Goal - gameObject.transform.position);
		/* //Just for debugging
		if (Input.GetKey(KeyCode.Alpha1))
			_direction.x = -1;
		else if (Input.GetKey(KeyCode.Alpha2))
			_direction.x = 1;
		*/

		_rigidBody.velocity = (_direction.normalized * _direction.magnitude * Speed);		
	}

	void PickRandomDirection()
	{
		_direction = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f));
	}
}
