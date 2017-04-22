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
		//print("try to kill player");
		//// IF: THIS PORTAL IS AN ENTRANCE
		//// IF: GAMEOBJECT TAG IS PLAYER
		//if (c.gameObject.tag.Contains("Player"))
		//{
		//	print("kill player");
		//	// do kill player
		//}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		print("kill player with trigger");
		c.SendMessage("kill");
	}
}