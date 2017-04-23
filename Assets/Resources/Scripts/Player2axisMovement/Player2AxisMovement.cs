using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2AxisMovement : MonoBehaviour
{
    public float _speed;
    private Vector3 mStartingPosition;
    private Vector3 mVelocity;
    private CollectionBehavior.Key Key = CollectionBehavior.Key.White;
    public bool zoomoncollection;

    //private const int MAX_SCORE = 999;
    private const float MIN_PLAYER_SIZE = .4f;

    private int myscore;
    private int Score
    {
        get { return myscore; }
        set 
        {
            myscore = value;
            if (myscore > CameraManager.FinalScore)
                CameraManager.FinalScore = myscore;
        }
    }
    private int _score = 0;
    public Text scoreText;
    public Text NotificationText;

    public float WinShrinkRate;// = .9f;
    public float WinShrinkWaitTime;// = .01f;
    public float WinStartSize;// = .2f;

    [Range(.01f,.99f)]
    public float CometReduceSizeFactor;

    [Range(.01f, .99f)]
    public float PlanetAbsordFactor;

    public int SizeToWin;

    private bool win = false;

    public GameObject burstPrefab;

    public delegate void PlayerLevelupHandler();
    public event PlayerLevelupHandler OnPlayerLevelup = delegate { };

    public delegate void AbsorbHandler();
    public event AbsorbHandler OnAbsorb = delegate { };

    void Start()
    {
        mVelocity = new Vector3(0, 0, 0);
        mStartingPosition = transform.position;
        //_score = (int) this.transform.localScale.x;
        GameObject.Find("Main Camera").GetComponent<CameraManager>().OnLevelup += this.OnLevelUp;
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
        {
            mVelocity.y = 0;
            var y = Input.GetAxis("Vertical");
            mVelocity.y = y;
        }
			

		if (Input.GetKey(KeyCode.RightArrow))
			mVelocity.x = 1;
		else if (Input.GetKey(KeyCode.LeftArrow))
			mVelocity.x = -1;
        else
        {
            mVelocity.x = 0;
            var x = Input.GetAxis("Horizontal");
            mVelocity.x = x;
        }
        
        GetComponent<Rigidbody2D>().velocity = (mVelocity.normalized * mVelocity.magnitude * _speed);

        scoreText.text = "" + Score;
    }

	public void scaleme(float s)
	{
		print("I got scaled: " + s);
        var size = this.transform.localScale.x * s;
        size = Mathf.Clamp(size, .01f, 2);
        this.transform.localScale = new Vector3(size,size,size);
        this._speed = 12 * size;
	}

    public void comethit()
    {
        var x = this.transform.localScale.x * CometReduceSizeFactor;
        x = Mathf.Max(x, MIN_PLAYER_SIZE);
        this.transform.localScale = new Vector3(x, x, x);
        var go = Instantiate(burstPrefab);
        go.transform.position = this.transform.position;
        var burst = go.GetComponent<BurstManager>();
        burst.MakeBurst(10, Color.white, this.transform.position, this.transform.localScale.x);

        var r = Random.Range(0, 6);
        switch (r)
        {
            case 0:
                NotificationText.text = "your dieting plan worked!";
                break;
            case 1:
                NotificationText.text = "";
                break;
            case 2:
                NotificationText.text = "the tiny people revolted and suceded from your planet.";
                break;
            case 3:
                NotificationText.text = "the goal is actually to obtain more real estate.";
                break;
            case 4:
                NotificationText.text = "Hint: to win, those objects should generally be avoided.";
                break;
            case 5:
                NotificationText.text = "aliens have taked your inhabitants hostage! eat more planets to boost your numbers.";
                break;
        }
    }


    public void absorb(float x)
    {
        
        var newx = this.transform.localScale.x + (x * PlanetAbsordFactor);
        var s = Mathf.Max(x, 1);
        Score += (int)s;
        this.transform.localScale = new Vector3(newx, newx, newx);

        if (win)
            return;

        if (this.transform.localScale.x >= SizeToWin)
        {
            OnPlayerLevelup();
            win = true;
        }

        if(!win)
        {
            var r = Random.Range(0, 7);
            switch (r)
            {
                case 0:
                    NotificationText.text = "that planet was no match for you!";
                    break;
                case 1:
                    NotificationText.text = "save some room for later.";
                    break;
                case 2:
                    NotificationText.text = "target annihilated!";
                    break;
                case 3:
                    NotificationText.text = "woah, slow down, this isn't a contest! actually, i guess it is... carry on.";
                    break;
                case 4:
                    NotificationText.text = "betchya can't eat just one!";
                    break;
                case 5:
                    NotificationText.text = "massive immigration occurred, causing a population swell.";
                    break;
                case 6:
                    NotificationText.text = "i know this story, but i think it started with an old lady who swollowed a fly...";
                    break;
            }
        }

    }

    public void OnLevelUp(int lvl)
    {
        if(lvl != 1)
        {
            win = true;
            StartCoroutine("StartOverRoutine");
        }
    }
    
    IEnumerator StartOverRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(0);
        var x = this.transform.localScale.x;
        while (x>.2f)
        {
            this.transform.localScale = new Vector3(x * WinShrinkRate, x * WinShrinkRate, x * WinShrinkRate);
            Score += 1;
            x = this.transform.localScale.x;
            yield return new WaitForSeconds(WinShrinkWaitTime);
        }
        this.transform.localScale = new Vector3(WinStartSize, WinStartSize, WinStartSize);

        //GameObject.Find("Spawner").GetComponent<WholeMapSpawner>().StartCoroutine("SpawnMap");
        yield return new WaitForSeconds(CameraManager.WinWaitTimeToRestart);

        win = false;
        yield return null;
    }

    private void kill()
    {
        if (win)
            return;
		var bgo = Instantiate(burstPrefab);
		bgo.transform.position = this.transform.position;
		var bm = bgo.GetComponent<BurstManager>();
		bm.MakeBurst(30, Color.white, this.transform.position, this.transform.localScale.x);
		GameObject.Find("Main Camera").GetComponent<CameraManager>().restartAfter3Seconds();

        Destroy(this.gameObject);
    }

    public void killByComet()
    {
        if (win)
            return;

        var r = Random.Range(0, 5);
        switch (r)
        {
            case 0:
                NotificationText.text = "better luck next time!";
                break;
            case 1:
                NotificationText.text = "there's this skill called dodge, you should learn it.";
                break;
            case 2:
                NotificationText.text = "it's okay, the many tine deaths you are responsible for never saw it coming.";
                break;
            case 3:
                NotificationText.text = "kjnasek;jnr;ak34j5nk34jtnk;j3qn6kqj3nktn";
                break;
            case 4:
                NotificationText.text = "let's try that again.";
                break;
        }

        kill();
    }

	public void killByPlanet()
	{
        if (win)
            return;

        var r = Random.Range(0, 5);
        switch (r)
        {
            case 0:
                NotificationText.text = "out of my way, small fry!";
                break;
            case 1:
                NotificationText.text = "bet you didn't read the instructions.";
                break;
            case 2:
                NotificationText.text = "pick on somebody your own size.";
                break;
            case 3:
                NotificationText.text = "the faint sounds of screaming were heard as you planet was crushed.";
                break;
            case 4:
                NotificationText.text = "did you forget to eat your spinach?";
                break;
            case 5:
                NotificationText.text = "maybe a smaller bit next time?";
                break;
        }

        kill();
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        //if(c.gameObject.tag == "Collection")
        //{
        //    var colgo = c.gameObject.GetComponent<CollectionBehavior>();
        //    var key = colgo.GetKey();
        //    this.GetComponent<SpriteRenderer>().color = CollectionBehavior.GetKeyColor(key);
        //    this.key = key;
        //    print("I became a: " + key.ToString());
        //    var collectionposition = c.gameObject.transform.position;
        //    var collectionscale = c.gameObject.transform.localScale.x;

        //    if(zoomoncollection)
        //    {
        //        var factor = CollectionBehavior.GetZoomFactor(key);
        //        if (factor == 1)
        //            GameObject.Find("Main Camera").GetComponent<SimpleZoom>().ZoomOut();
        //        if (factor == -1)
        //            GameObject.Find("Main Camera").GetComponent<SimpleZoom>().ZoomIn();
        //    }

        //    Destroy(c.gameObject);

        //    var bgo = Instantiate(burstPrefab);
        //    bgo.transform.position = collectionposition;
        //    var bm = bgo.GetComponent<BurstManager>();
        //    bm.MakeBurst(10, CollectionBehavior.GetKeyColor(key), collectionposition, c.gameObject.transform.localScale.x);

            

        //    if(CollectionBehavior.Key.Blue == key)
        //    {
        //        var r = Random.Range(0, 4);
        //        switch (r)
        //        {
        //            case 0:
        //                NotificationText.text = "You found a blue key. go find a blue gate!";
        //                break;
        //            case 1:
        //                NotificationText.text = "Why so blue?";
        //                break;
        //            case 2:
        //                NotificationText.text = "Whats this? A blue key? for me?!";
        //                break;
        //            case 3:
        //                NotificationText.text = "";
        //                break;
        //        }
        //    }
        //    else if (CollectionBehavior.Key.Green == key)
        //    {
        //        var r = Random.Range(0, 4);
        //        switch (r)
        //        {
        //            case 0:
        //                NotificationText.text = "You found a green key. go find a green gate!";
        //                break;
        //            case 1:
        //                NotificationText.text = "This green key will unlock a blue gate! MADE YOU LOOK! HAHAHA";
        //                break;
        //            case 2:
        //                NotificationText.text = "rawr? wrong game? oh, just find the gate and get this over...";
        //                break;
        //            case 3:
        //                NotificationText.text = "";
        //                break;
        //        }
        //    }
        //}

        //if (c.gameObject.tag == "Goal")
        //{
        //    var bgo = Instantiate(burstPrefab);
        //    bgo.transform.position = c.gameObject.transform.position;
        //    var bm = bgo.GetComponent<BurstManager>();
        //    bm.MakeBurst(10, Color.yellow, c.gameObject.transform.position, c.gameObject.transform.localScale.x);

        //    Destroy(c.gameObject);

        //    _score++;
        //    var r = Random.Range(0, 4);
        //    switch(r)
        //    {
        //        case 0:
        //            NotificationText.text = "Great! you got a yellow block, Go find keys and get more yellow blocks";
        //            break;
        //        case 1:
        //            NotificationText.text = "All Hail the Yellow Blocks";
        //            break;
        //        case 2:
        //            NotificationText.text = "GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD GOLD";
        //            break;
        //        case 3:
        //            NotificationText.text = "One step closer to greatness!";
        //            break;
        //    }

        //    if(_score == MAX_SCORE)
        //    {
        //        kill();
        //    }

        //    absorb(10);
        //}
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
