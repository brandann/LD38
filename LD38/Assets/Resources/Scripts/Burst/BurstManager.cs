using UnityEngine;
using System.Collections;

public class BurstManager : MonoBehaviour {

	// PREFAB TO BURST
	// CAN BE ANYTHING THAT HAS A SPRITE AND A TRANSFORM!
	// BURST MANAGER DOES NOT KEEP TRACK OF ITS SPAWNED BURSTS
    public GameObject mBurstPrefab;
    
    // COUNTS HOW MANY BURST ARE SCEDULUED TO GO
    private float mBurstEmitterCount;
    
    // COLOR TO SET THE BURST OBJECT
    // THIS SHOULD BE OPTIONAL IN THE FUTURE BUT
    // RIGHT NOW IT HAPPENS IF YOU LIKE IT OR NOT
    private Color mColor = Color.yellow;

    private bool mBurstReady = false;

	private float mBurstScale = 1;

	private BurstBehavior.eBurstSprites mBurstSprite;

    // Use this for initialization
    void Start () {
        if (!mBurstReady)
            return;

    	// MAKE ALL THE BURST POINTS
		for(int i = 0; i < mBurstEmitterCount; i++){
			makeBurstPoint ();
		}
		
		// DESTROY MYSELF WHEN I HAVE MADE ALL THE BURSTS
		Destroy(this.gameObject);
    }

	// MAKES A BURST OBJECT BY INSTANCIATING A GAMEOBJECT
    private void makeBurstPoint() {
		GameObject burstGO = Instantiate(mBurstPrefab) as GameObject;
		burstGO.transform.position = this.transform.position;
		burstGO.transform.Rotate(new Vector3(0,0,Random.Range(0,360)));
		burstGO.GetComponent<SpriteRenderer>().color = mColor;
		burstGO.GetComponent<SpriteRenderer>().sprite = GetSprite(burstGO, mBurstSprite);
		burstGO.transform.localScale *= mBurstScale;
    }

	private Sprite GetSprite(GameObject burstObject, BurstBehavior.eBurstSprites sprite)
	{
		if (sprite == BurstBehavior.eBurstSprites.Random)
			return GetRandomSprite(burstObject);
		var behavior = burstObject.GetComponent<BurstBehavior>();
		var sprites = behavior.BurstSprites;
		return sprites[(int) sprite];
	}

	private Sprite GetRandomSprite(GameObject burstObject)
	{
		var behavior = burstObject.GetComponent<BurstBehavior>();
		var sprites = behavior.BurstSprites;
		var rand = Random.Range(0, sprites.Length);
		return sprites[rand];
	}

    public void MakeBurst(int burstCount, Color burstColor, Vector3 position/*, float burstInitSize -- NOT USED YET*/)
    {
		MakeBurst(burstCount, burstColor, position, 1);
	}

	public void MakeBurst(int burstCount, Color burstColor, Vector3 position, float burstScale)
	{
		MakeBurst(burstCount, burstColor, position, burstScale, BurstBehavior.eBurstSprites.Random);
	}

	public void MakeBurst(int burstCount, Color burstColor, Vector3 position, float burstScale, BurstBehavior.eBurstSprites sprite)
	{
		this.transform.position = position;
		mBurstEmitterCount = burstCount;
		mColor = burstColor;
		mBurstScale = burstScale;
		mBurstReady = true;

		mBurstSprite = sprite;
	}
}
