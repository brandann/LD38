using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player2AxisMovement : MonoBehaviour
{
    private float _speed = 12;
    private Vector3 mStartingPosition;
    private Vector3 mVelocity;
    private CollectionBehavior.Key Key = CollectionBehavior.Key.White;
    public bool zoomoncollection;

    void Start()
    {
        mVelocity = new Vector3(0, 0, 0);
        mStartingPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		// GET THE VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
		if (Input.GetKey(KeyCode.UpArrow))
			mVelocity.y = 1;
		else if (Input.GetKey(KeyCode.DownArrow))
			mVelocity.y = -1;
		else
			mVelocity.y = 0;

		if (Input.GetKey(KeyCode.RightArrow))
			mVelocity.x = 1;
		else if (Input.GetKey(KeyCode.LeftArrow))
			mVelocity.x = -1;
		else
			mVelocity.x = 0;
        
        GetComponent<Rigidbody2D>().velocity = (mVelocity.normalized * mVelocity.magnitude * _speed);
    }

	public void scaleme(float s)
	{
		print("I got scaled: " + s);
        var size = this.transform.localScale.x * s;
        size = Mathf.Clamp(size, .01f, 2);
        this.transform.localScale = new Vector3(size,size,size);
        this._speed = 12 * size;
	}

	public GameObject burstPrefab;

	public void kill()
	{
		var bgo = Instantiate(burstPrefab);
		bgo.transform.position = this.transform.position;
		var bm = bgo.GetComponent<BurstManager>();
		bm.MakeBurst(30, Color.white, this.transform.position, this.transform.localScale.x);
		GameObject.Find("Main Camera").GetComponent<CameraManager>().restartAfter3Seconds();
		Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag == "Collection")
        {
            var colgo = c.gameObject.GetComponent<CollectionBehavior>();
            var key = colgo.GetKey();
            this.GetComponent<SpriteRenderer>().color = CollectionBehavior.GetKeyColor(key);
            this.key = key;
            print("I became a: " + key.ToString());
            var collectionposition = c.gameObject.transform.position;
            var collectionscale = c.gameObject.transform.localScale.x;

            if(zoomoncollection)
            {
                var factor = CollectionBehavior.GetZoomFactor(key);
                if (factor == 1)
                    GameObject.Find("Main Camera").GetComponent<SimpleZoom>().ZoomOut();
                if (factor == -1)
                    GameObject.Find("Main Camera").GetComponent<SimpleZoom>().ZoomIn();
            }

            Destroy(c.gameObject);

            var bgo = Instantiate(burstPrefab);
            bgo.transform.position = collectionposition;
            var bm = bgo.GetComponent<BurstManager>();
            bm.MakeBurst(10, CollectionBehavior.GetKeyColor(key), collectionposition, c.gameObject.transform.localScale.x);
        }

        if (c.gameObject.tag == "Goal")
        {
            var bgo = Instantiate(burstPrefab);
            bgo.transform.position = c.gameObject.transform.position;
            var bm = bgo.GetComponent<BurstManager>();
            bm.MakeBurst(10, Color.yellow, c.gameObject.transform.position, c.gameObject.transform.localScale.x);

            Destroy(c.gameObject);
        }
    }

    private CollectionBehavior.Key _key = CollectionBehavior.Key.White;
    public CollectionBehavior.Key key 
    {
        get
        {
            return _key;
        }
        set
        {
            this._key = value;
            this.GetComponent<SpriteRenderer>().color = CollectionBehavior.GetKeyColor(value);
        }
    }

    public void ResetKey()
    {
        this.key = CollectionBehavior.Key.White;
    }
}
