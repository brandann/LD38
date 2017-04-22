using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {

	// KEEPS TRACK OF THE ROTATION DIRECTION
    public int RotationDirection;
    
    // KEEPS TRACK OF THE ROTATION SPEED
    public float RotationSpeed;

	// SETS IF THE OBJECT IS CURRENTLY ROTATING OR NOT
    private bool mRotationActive = true;
    private Vector3 mRotation;

	void Start () {
        SetRotation(RotationDirection, RotationSpeed, true);
    }
	
	void Update () {
        if (mRotationActive)
        {
			this.transform.Rotate(mRotation * Time.deltaTime);
		}
	}

    // SET THE ROTATION PARAMS FOR THE GAMEOBJECT
    // DIRECTION (INT) -1,0,1 ROATION DIRECTION RESPECTIVLY
    // SPEED (FLOAT) ROTATION PER UPDATE <- SCALED VIA TIME.DELTATIME
    // ACTIVE (BOOL) IS THE ROATION ACTIVE?
    public void SetRotation(int direction, float speed, bool active)
    {
        mRotation = new Vector3(0, 0, -1 * direction * speed);
        mRotationActive = active;
    }

    // SETS THE ACTIVE PARAM OF THE ROTATION
    public void SetRotationEnabled(bool active)
    {
        mRotationActive = active;
    }

    // REVERSE THE ROTATION
    public void ReverseRotation()
    {
        mRotation = -1 * mRotation;
    }
    
}
