using UnityEngine;
using System.Collections;
using System;

public class SimpleZoom : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
			ZoomOut();
		if (Input.GetKeyDown(KeyCode.W))
			ZoomIn();
	}

	private void ZoomOut()
	{
		var cam = this.GetComponent<Camera>();
		if (null != cam)
			cam.orthographicSize *= .9f;
		player.transform.localScale *= .9f;
		player.SendMessage("scaleme", .9f);
	}

	private void ZoomIn()
	{
		var cam = this.GetComponent<Camera>();
		if (null != cam)
			cam.orthographicSize *= 1.1f;
		player.transform.localScale *= 1.1f;
		player.SendMessage("scaleme", 1.1f);
	}
}
