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
		// IF: THIS PORTAL IS AN ENTRANCE
		// IF: GAMEOBJECT TAG IS PLAYER
		if (c.gameObject.tag.Contains("Player"))
		{
			// do kill player
		}
	}
}