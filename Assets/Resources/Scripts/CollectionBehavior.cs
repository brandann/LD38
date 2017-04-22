using UnityEngine;
using System.Collections;

public class CollectionBehavior : MonoBehaviour {

    public enum Key { White, Blue, Green }
    private Key myKey = Key.Blue;

    public Color BlueColor = Color.blue;
    public Color GreenColor = Color.green;

	// Use this for initialization
	void Start () {
        //myKey = Key.White;
        //this.GetComponent<SpriteRenderer>().color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnRandomKey()
    {
        var r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                myKey = Key.Blue;
                this.GetComponent<SpriteRenderer>().color = BlueColor;
                break;
            case 1:
                myKey = Key.Green;
                this.GetComponent<SpriteRenderer>().color = GreenColor;
                break;
            case 2:
                print("bad");
                break;
        }
    }

    public void SpawnKey(Key k)
    {
        myKey = k;
    }

    public Key GetKey()
    {
        if (myKey == Key.White)
            SpawnRandomKey();
        return myKey;
    }

    public Color GetKeyColor()
    {
        return this.GetComponent<SpriteRenderer>().color;
    }

    public static Color GetKeyColor(Key k)
    {
        if (Key.Blue == k)
            return Color.blue;
        if (Key.Green == k)
            return Color.green;
        return Color.white;
    }

    public static int GetZoomFactor(Key k)
    {
        if (Key.Blue == k)
            return 1;
        if (Key.Green == k)
            return -1;
        return 0;
    }
}
