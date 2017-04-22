using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private static Transform mCameraTransform;

    // How long the object should shake for.
	private static float mShakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
	private static float mShakeRadius = 1f;
	private static float mDecreaseFactor = .9f;

    static Vector3 _mOriginalPos;
    
    void Start()
    {

    }

    void Awake()
    {
        if (mCameraTransform == null)
        {
            mCameraTransform = GameObject.Find("Main Camera").GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        _mOriginalPos = mCameraTransform.localPosition;
    }

    void Update()
    {
        if(mShakeDuration != 0 && Time.deltaTime == 0)
        {
        	mShakeDuration = 0;
        }
        else if (mShakeDuration > 0)
        {
            mCameraTransform.localPosition = _mOriginalPos + Random.insideUnitSphere * mShakeRadius;
            mShakeDuration -= Time.deltaTime * mDecreaseFactor;
        }
        else
        {
            mShakeDuration = 0f;
        }
    }

	// SHAKE THE CAMERA FOR A DURATION OF 'F'
    public static void Shake(float f = .1f)
    {
        mShakeDuration = f;
		_mOriginalPos = mCameraTransform.localPosition;
    }
}
