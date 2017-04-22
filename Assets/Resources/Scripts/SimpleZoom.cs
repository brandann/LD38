using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SimpleZoom : MonoBehaviour {

	public GameObject player;
    public Slider zoomUI;

    public const float CameraMax = 20;
    public const float CameraMin = .1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
			ZoomOut();
		if (Input.GetKeyDown(KeyCode.W))
			ZoomIn();

        zoomUI.value =this.GetComponent<Camera>().orthographicSize / CameraMax;
	}

	private void ZoomOut()
	{
		var cam = this.GetComponent<Camera>();
        var s = Mathf.Clamp(cam.orthographicSize * .9f, CameraMin, CameraMax);
        cam.orthographicSize = s;
		player.SendMessage("scaleme", .9f);
        UpdateSlider();
	}

	private void ZoomIn()
	{
		var cam = this.GetComponent<Camera>();
        var s = Mathf.Clamp(cam.orthographicSize * 1.1f, CameraMin, CameraMax);
        cam.orthographicSize = s;
        player.SendMessage("scaleme", 1.1f);
        UpdateSlider();
	}

    private void UpdateSlider()
    {
        zoomUI.value = CameraMax / this.GetComponent<Camera>().orthographicSize;
    }
}
