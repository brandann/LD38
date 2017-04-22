using UnityEngine;
using System.Collections;
using System;

public class ZoomVisible : MonoBehaviour
{

    public GameObject player;
    public float ZoomInFactor = 1.1f;
    public float ZoomOutFactor = 0.9f;
    public float CurrentZoom = 1;

    private Camera _camera;

    // Use this for initialization
    void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ZoomOut();
        if (Input.GetKeyDown(KeyCode.W))
            ZoomIn();
    }

    private void ZoomOut()
    {
        CurrentZoom *= ZoomOutFactor;
        _camera.orthographicSize *= ZoomOutFactor;
        player.transform.localScale *=  ZoomOutFactor;         
        player.SendMessage("scaleme", ZoomOutFactor);
    }

    private void ZoomIn()
    {
        CurrentZoom *= ZoomInFactor;
        _camera.orthographicSize *= ZoomInFactor;
        player.transform.localScale *= ZoomInFactor;
        player.SendMessage("scaleme", ZoomInFactor);
    }
}
