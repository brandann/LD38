using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Schooler : MonoBehaviour {

	static Vector3 GlobalGoal;
	private Vector3 _goal;
	private GameObject Player;
	private float Speed = 9;
	private Vector2 _direction = new Vector2(0, 0).normalized;
	Rigidbody2D _rigidBody;
	List<Schooler> _collidingSchoolers;
	private float _lastRealignment;
	private float _realignmentDuration = 2f;
	private float _spacing = 0.5f;
	public GameObject BurstManagerPrefab;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody2D>();
		PickRandomDirection();
		_collidingSchoolers = new List<Schooler>();
		_lastRealignment = Time.timeSinceLevelLoad;
		Player = GameObject.Find("Player");
		_goal = Player.transform.position;
	}

	private void Realign()
	{
		if (Player == null)
		{
			Debug.LogError("where the hell did the player go?");
			return;
		}

		_goal = Player.transform.position;
		_lastRealignment = Time.timeSinceLevelLoad;

	}
	
	// Update is called once per frame
	void Update () {
		if( (Time.timeSinceLevelLoad - _lastRealignment) > _realignmentDuration)
		{
			Realign();
		}

		_direction = (_goal - gameObject.transform.position);
		/* //Just for debugging
		if (Input.GetKey(KeyCode.Alpha1))
			_direction.x = -1;
		else if (Input.GetKey(KeyCode.Alpha2))
			_direction.x = 1;
		*/

		_rigidBody.velocity = (_direction.normalized * Speed);		
	}

	void PickRandomDirection()
	{
		_direction = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f));
	}

	public void HandleCollisionFromDetector(Collider2D other)
	{
		var collidedSchooler = other.GetComponent<Schooler>();
		if (collidedSchooler == null)
			return;

		_collidingSchoolers.Add(collidedSchooler);
		print("add collision in schooler");
	}

	public void HandleRemoveCollisionFromDetector(Collider2D other)
	{
		var collidedSchooler = other.GetComponent<Schooler>();
		if (collidedSchooler == null)
			return;

		_collidingSchoolers.Remove(collidedSchooler);
		print("remove collision in schooler");

	}

	void OnCollisionEnter2D(Collision2D other)	
		{
		if (other.gameObject.tag.Contains("Player"))
		{
			other.gameObject.SendMessage("SchoolerHit");
			var go = Instantiate(BurstManagerPrefab);
			go.transform.position = this.transform.position;
			var burst = go.GetComponent<BurstManager>();
			burst.MakeBurst(10, Color.magenta, this.transform.position, this.transform.localScale.x);
			Destroy(this.gameObject);
		}
	}
}
