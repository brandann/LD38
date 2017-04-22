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
        var s = Mathf.Clamp(cam.orthographicSize * .9f, 0.1f, 20);
        cam.orthographicSize = s;
		player.SendMessage("scaleme", .9f);
	}

	private void ZoomIn()
	{
		var cam = this.GetComponent<Camera>();
        var s = Mathf.Clamp(cam.orthographicSize * 1.1f, 0.1f, 20);
        cam.orthographicSize = s;
        player.SendMessage("scaleme", 1.1f);
	}
}
