using UnityEngine;
using System.Collections;
using System.Timers;

public class SimpleBlockSpawner : MonoBehaviour {
	public GameObject Player;
	[Space(10)]

	public float SpawnedItemSpeed;

	#region PrefabOne
	public GameObject PrefabOne;
	[RangeAttribute(0,1)]
	public float MinSizePrefabOne;	
	public float MaxSizePrefabOne;
	[RangeAttribute(1,600)]
	public float SpawnRateSecondsPrefabOne;	
	private float _lastSpawnTimePrefabOne;
	[Space(10)]
	#endregion

	#region PrefabTwo
	public GameObject PrefabTwo;
	[RangeAttribute(0, 1)]
	public float MinSizePrefabTwo;
	public float MaxSizePrefabTwo;
	[RangeAttribute(1, 600)]
	public float SpawnRateSecondsPrefabTwo;
	private float _lastSpawnTimePrefabTwo;
	[Space(10)]
	#endregion

	#region PrefabThree
	public GameObject PrefabThree;
	[RangeAttribute(0, 1)]
	public float MinSizePrefabThree;
	public float MaxSizePrefabThree;
	[RangeAttribute(1, 600)]
	public float SpawnRateSecondsPrefabThree;
	private float _lastSpawnTimePrefabThree;
	[Space(10)]
	#endregion

	#region PrefabFour
	public GameObject PrefabFour;
	[RangeAttribute(0, 1)]
	public float MinSizePrefabFour;
	public float MaxSizePrefabFour;
	[RangeAttribute(1, 600)]
	public float SpawnRateSecondsPrefabFour;
	private float _lastSpawnTimePrefabFour;
	[Space(10)]
	#endregion
	

	#region Schooler Enemy
	public GameObject SchoolingEnemyPrefab;
	public float SchoolerSpawnDuration;
	public int SchoolerSpawnLimit;
	private float _lastSchoolerSpawnTime;
	private int _schoolersSpawned;
	#endregion

	// Use this for initialization
	void Start () {
			
		_lastSpawnTimePrefabOne = 
		_lastSpawnTimePrefabTwo	= 
		_lastSpawnTimePrefabThree =
		_lastSpawnTimePrefabFour = Time.timeSinceLevelLoad;


	}

	private void SpawnFromCircleToPlayer(GameObject prefab)
	{
		if (prefab == null)
			return;

		SpawnFromCircleToPlayer(prefab, Random.Range(0.1f, 1f), Random.Range(1f, 2f), SpawnedItemSpeed);
	}

	private void SpawnFromCircleToPlayer(GameObject prefab, float minSize, float maxSize, float speed)
	{
		if (prefab == null)
			return;

		var go = Instantiate(prefab);
		var randvec = Random.insideUnitCircle;
		randvec.Normalize();
		randvec *= this.transform.localScale.x / 2;

		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize * minSize, psize * maxSize);

		go.transform.localScale = new Vector3(randscale, randscale, 1);

		var up = Player.transform.position - go.transform.position;
		go.transform.up = up;
		go.GetComponent<Rigidbody2D>().velocity = up * speed * Player.transform.localScale.x;

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeSinceLevelLoad - _lastSpawnTimePrefabOne > SpawnRateSecondsPrefabOne )
		{
			SpawnFromCircleToPlayer(PrefabOne);
			_lastSpawnTimePrefabOne = Time.timeSinceLevelLoad;
		}

		if (Time.timeSinceLevelLoad - _lastSpawnTimePrefabTwo > SpawnRateSecondsPrefabTwo)
		{
			SpawnFromCircleToPlayer(PrefabTwo);
			_lastSpawnTimePrefabTwo = Time.timeSinceLevelLoad;
		}

		if (Time.timeSinceLevelLoad - _lastSpawnTimePrefabThree > SpawnRateSecondsPrefabThree)
		{
			SpawnFromCircleToPlayer(PrefabThree);
			_lastSpawnTimePrefabThree = Time.timeSinceLevelLoad;
		}

		if (Time.timeSinceLevelLoad - _lastSpawnTimePrefabFour > SpawnRateSecondsPrefabFour)
		{
			SpawnFromCircleToPlayer(PrefabFour);
			_lastSpawnTimePrefabFour = Time.timeSinceLevelLoad;
		}


	}
	/*

	private void SpawnSchooler()
	{
		if (SchoolingEnemyPrefab == null)
		{
			Debug.LogError("No schooler to spawn");
			return;
		}	

		var go = Instantiate(SchoolingEnemyPrefab);
		var randvec = Random.insideUnitCircle;
		randvec = Random.Range(this.transform.localScale.x / 2, this.transform.localScale.x / 1.5f ) * randvec ;

		go.transform.position = randvec;	

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize / 10, psize *1.1f);
		go.transform.localScale = new Vector3(randscale, randscale, randscale);

		//Bookeeping
		_lastSchoolerSpawnTime = Time.timeSinceLevelLoad;
		_schoolersSpawned++;
	}

	

	private void SpawnWall()
	{
		//var go = Instantiate(WallBlockPrefab);
		var randvec = Random.insideUnitCircle;
		randvec = Random.Range(this.transform.localScale.x / 5, this.transform.localScale.x) * randvec;

		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize / 10, psize * 5);
		go.transform.localScale = new Vector3(randscale, randscale, randscale);
	}

	private void SpawnBlock()
	{
		var go = Instantiate(PrefabOne);
		var randvec = Random.insideUnitCircle;
		randvec.Normalize();
		randvec *= this.transform.localScale.x / 2;

		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize/10, psize*2f);

		go.transform.localScale = new Vector3(randscale, randscale, randscale);

		var up = Player.transform.position - go.transform.position;
		go.transform.up = up;
		//go.GetComponent<Rigidbody2D>().velocity = up * EnemySpeed * Player.transform.localScale.x;
	}

	private void SpawnCollection()
	{
		//var go = Instantiate(CollectionBlockPrefab);
        //go.GetComponent<CollectionBehavior>().SpawnRandomKey();
		var randvec = Random.insideUnitCircle;
		randvec = Random.Range(this.transform.localScale.x/5, this.transform.localScale.x) * randvec;
		
		go.transform.position = randvec;

		var psize = Player.transform.lossyScale.x;
		var randscale = Random.Range(psize *.9f, psize * 1.1f);
		go.transform.localScale = new Vector3(randscale, randscale, randscale);

        
	}
	*/
}
