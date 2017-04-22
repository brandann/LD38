using UnityEngine;
using System.Collections;

public class SimpleBlockSpawner : MonoBehaviour {

	public GameObject SpawnedBlockPrefab;
	public GameObject Player;

	float EnemyStarttime = 0;
	public float EnemyDuration;
	public float EnemySpeed;

	public GameObject CollectionBlockPrefab;
	float CollectionStartTime = 0;
	public float CollectionDuration;

	public GameObject WallBlockPrefab;
	float WallStartTime = 0;
	public float WallDuration;

	// Use this for initialization
	void Start () {
		EnemyStarttime = Time.timeSinceLevelLoad;
		CollectionStartTime = Time.timeSinceLevelLoad;
		WallStartTime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if((Time.timeSinceLevelLoad - EnemyStarttime) > EnemyDuration)
		{
			SpawnBlock();
			EnemyStarttime = Time.timeSinceLevelLoad;
		}

		if ((Time.timeSinceLevelLoad - CollectionStartTime) > CollectionDuration)
		{
			SpawnCollection();
			CollectionStartTime = Time.timeSinceLevelLoad;
		}

		if ((Time.timeSinceLevelLoad - WallStartTime) > WallDuration)
		{
			SpawnWall();
			WallStartTime = Time.timeSinceLevelLoad;
		}
	}

	private void SpawnWall()
	{
		var go = Instantiate(WallBlockPrefab);
		var randvec = Random.insideUnitCircle;
		randvec = Random.Range(this.transform.localScale.x / 5, this.transform.localScale.x) * randvec;

		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize / 10, psize * 5);
		go.transform.localScale = new Vector3(randscale, randscale, randscale);
	}

	private void SpawnBlock()
	{
		var go = Instantiate(SpawnedBlockPrefab);
		var randvec = Random.insideUnitCircle;
		randvec.Normalize();
		randvec *= this.transform.localScale.x / 2;

		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize/10, psize*5);

		go.transform.localScale = new Vector3(randscale, randscale, randscale);

		var up = Player.transform.position - go.transform.position;
		go.transform.up = up;
		go.GetComponent<Rigidbody2D>().velocity = up * EnemySpeed * Player.transform.localScale.x;
	}

	private void SpawnCollection()
	{
		var go = Instantiate(CollectionBlockPrefab);
        go.GetComponent<CollectionBehavior>().SpawnRandomKey();
		var randvec = Random.insideUnitCircle;
		randvec = Random.Range(this.transform.localScale.x/5, this.transform.localScale.x) * randvec;
		
		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize *.9f, psize * 1.1f);
		go.transform.localScale = new Vector3(randscale, randscale, randscale);

        
	}
}
