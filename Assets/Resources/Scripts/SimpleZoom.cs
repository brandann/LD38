using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SimpleZoom : MonoBehaviour {

	public GameObject player;
    public Slider zoomUI;
	//private float zoomRate = .9f;

    //public const float CameraMax = 20;
    //public const float CameraMin = .1f;

    private float sizetowin;

	// Use this for initialization
	void Start () {
        sizetowin = player.GetComponent<Player2AxisMovement>().SizeToWin;
	}
	
	// Update is called once per frame
	void Update () {
		
        //if (Input.GetKeyDown(KeyCode.Q))
        //ZoomOut();
        //if (Input.GetKeyDown(KeyCode.W))
        //ZoomIn();


        zoomUI.value = 1 - (player.transform.localScale.x / sizetowin);      
	}

	public void ZoomOut()
	{
        //var cam = this.GetComponent<Camera>();
        //var s = Mathf.Clamp(cam.orthographicSize * zoomRate, CameraMin, CameraMax);
        //cam.orthographicSize = s;
        //player.SendMessage("scaleme", zoomRate);
        //UpdateSlider();
	}

	public void ZoomIn()
	{
        //var cam = this.GetComponent<Camera>();
        //var s = Mathf.Clamp(cam.orthographicSize * 1.1f, CameraMin, CameraMax);
        //cam.orthographicSize = s;
        //player.SendMessage("scaleme", 1.1f);
        //UpdateSlider();
	}

    private void UpdateSlider()
    {
        //zoomUI.value = CameraMax / this.GetComponent<Camera>().orthographicSize;
    }
}
