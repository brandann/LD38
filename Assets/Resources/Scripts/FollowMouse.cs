using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        this.transform.position = mousePosition;
	}
}
