using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WholeMapSpawner : MonoBehaviour {

	#region PrefabOne
	public GameObject PrefabOne;
	public float MinSizePrefabOne;
	public float MaxSizePrefabOne;
	public int NumberOfItemsToSpawn;
	public int MinX;
	public int MinY;
	public int MaxX;
	public int MaxY;
	public float MinSpeed;
	public float MaxSpeed;
	#endregion

	#region ContinuousPrefab
	public GameObject ContinuousPrefab;
	public float SpawnTimeSecondsContinuousPrefab;
	public int MaxContinuousPrefabToSpawn;	
	public float MinContinuousScale;
	public float MaxContinuousScale;
	private int _totalContinousPrefabsSpawned;
	#endregion

	#region Levels
	private LevelVariables _lvl1, _lvl2, _lvl3;
	private Dictionary<int, LevelVariables> _levelLookup;
	#endregion 

	private const float BUFFER_SIZE = 10;
	private GameObject _player;
	private CameraManager _cameraManager;

	private enum BufferSide { Left, Right, Up, Down };

	// Use this for initialization
	void Start() {

		CreateLevels();
		

		_cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
		_cameraManager.OnLevelup += HandleLevelUp;
		

		_player = GameObject.Find("Player");

		if (_player == null)
		{
			Debug.LogError("unable to find player");
			return;
		}

		if (PrefabOne == null)
			return;			
	}

	void HandleLevelUp(int lvlNum)
	{
		LevelVariables nextLvl = null;
		bool foundLevel = _levelLookup.TryGetValue(lvlNum, out nextLvl);

		if(foundLevel == false)
		{
			Debug.LogError("unable to find level");
			return;
		}

		LoadLevelParameters(nextLvl);
		PopulateInitialWorldAtOnce();
		StartCoroutine("SpawnContinuous");
	}


	void LoadLevelParameters(LevelVariables lvl)
	{
		MinSizePrefabOne = lvl.MinSizePrefabOne;
		MaxSizePrefabOne = lvl.MaxSizePrefabOne;
		NumberOfItemsToSpawn = lvl.NumberOfItemsToSpawn;
		//MinX = lvl.MinX;
		//MinY = lvl.MinY;
		//MaxX = lvl.MaxX;
		//MaxY = lvl.MaxY;
		MinSpeed = lvl.MinSpeed;
		MaxSpeed = lvl.MaxSpeed;
	}

	void PopulateInitialWorldAtOnce()
	{
		BufferSide currSide;

		for (int i = 0; i < NumberOfItemsToSpawn; ++i)
		{
			currSide = (BufferSide)(i % 4);
			SpawnPrefab(PrefabOne, currSide);
		}
	}

	public IEnumerator SpawnContinuous()
	{
		while (_totalContinousPrefabsSpawned < MaxContinuousPrefabToSpawn)
		{
			yield return new WaitForSeconds(SpawnTimeSecondsContinuousPrefab);
			GameObject go = SpawnPrefabAwayFromPlayer(ContinuousPrefab, BUFFER_SIZE);
			GiveRandomSpeed(go);
			GiveRandomScale(go, MinContinuousScale, MaxContinuousScale);
			_totalContinousPrefabsSpawned++;

		}
		yield return null;
	}

	// COROUTINE FOR SPEED MOD
	private IEnumerator SpawnInitialMap()
	{
		// WAIT FOR THE MOD DURATION TO FINISH

		BufferSide currSide;

		for (int i = 0; i < NumberOfItemsToSpawn; ++i)
		{
			currSide = (BufferSide)(i % 4);
			SpawnPrefab(PrefabOne, currSide);
			yield return new WaitForSeconds(0f);
		}

		yield return null;
	}

	GameObject SpawnPrefabAwayFromPlayer(GameObject prefab, float distance)
	{

		Vector3 randPosition;
		do
		{
			var randX = Random.Range(MinX, MaxX);
			var randY = Random.Range(MinY, MaxY);
			randPosition = new Vector3(randX, randY, 1);
		} while ((_player.transform.position - randPosition).magnitude < distance);

		var go = (GameObject)Object.Instantiate(prefab,randPosition,Quaternion.identity);		
		return go;
	}

	void GiveRandomSpeed(GameObject go)
	{
		float randomSpeed = Random.Range(MinSpeed, MaxSpeed);
		go.GetComponent<Rigidbody2D>().velocity = new Vector3(randomSpeed, randomSpeed, 0);
	}

	void GiveRandomScale(GameObject go, float minScale, float maxScale)
	{
		float randomScale = Random.Range(minScale, maxScale);
		go.transform.localScale = new Vector3(randomScale, randomScale, 1);
	}


	void SpawnPrefab(GameObject prefab, BufferSide side)
	{
		Vector3 location = new Vector3();

		switch (side)
		{
			case BufferSide.Left:
				location = new Vector3(Random.Range(MinX, _player.transform.position.x - BUFFER_SIZE), Random.Range(MinY, MaxY), 0);
				break;
			case BufferSide.Right:
				location = new Vector3(Random.Range(_player.transform.position.x + BUFFER_SIZE, MaxY), Random.Range(MinY, MaxY), 0);
				break;
			case BufferSide.Up:
				location = new Vector3(Random.Range(MinX, MaxX), Random.Range(_player.transform.position.y + BUFFER_SIZE, MaxY), 0);
				break;
			case BufferSide.Down:
				location = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, _player.transform.position.y - BUFFER_SIZE), 0);
				break;
		}

		var go = Object.Instantiate(prefab);
		go.transform.position = location;		
		GiveRandomScale(go, MinSizePrefabOne, MaxSizePrefabOne);		
		GiveRandomSpeed(go);		
	}

	private void CreateLevels()
	{
		_lvl1 = new LevelVariables {
			MinSizePrefabOne = .01f,
			MaxSizePrefabOne = 2f,
			NumberOfItemsToSpawn = 30,
			MinSpeed = -4f,
			MaxSpeed = 2f,
			SpawnTimeSecondsContinuousPrefab = 2,
			MaxContinuousPrefabToSpawn = 20,
			MinContinuousScale = 0.2f,
			MaxContinuousScale = 1f
		} ;

		_lvl2 = new LevelVariables
		{
			MinSizePrefabOne = .01f,
			MaxSizePrefabOne = 8f,
			NumberOfItemsToSpawn = 40,
			MinSpeed = -4f,
			MaxSpeed = 4f,
			SpawnTimeSecondsContinuousPrefab = 2,
			MaxContinuousPrefabToSpawn = 20,
			MinContinuousScale = 0.2f,
			MaxContinuousScale = 1.8f
		};

		_lvl3 = new LevelVariables
		{
			MinSizePrefabOne = .05f,
			MaxSizePrefabOne = 8f,
			NumberOfItemsToSpawn = 40,
			MinSpeed = -4f,
			MaxSpeed = 4f,
			SpawnTimeSecondsContinuousPrefab = 2,
			MaxContinuousPrefabToSpawn = 20,
			MinContinuousScale = 0.2f,
			MaxContinuousScale = 1.8f
		};

		_levelLookup = new Dictionary<int, LevelVariables>();
		_levelLookup.Add(1, _lvl1);
		_levelLookup.Add(2, _lvl2);
		_levelLookup.Add(3, _lvl3);
	}

	private class LevelVariables
	{
		public float MinSizePrefabOne { get;  set; }
		public float MaxSizePrefabOne { get;  set; }
		public int NumberOfItemsToSpawn { get;  set; }
		//public int MinX { get;  set; }
		//public int MinY { get;  set; }
		//public int MaxX { get;  set; }
		//public int MaxY { get;  set; }
		public float MinSpeed { get;  set; }
		public float MaxSpeed { get;  set; }

		public float SpawnTimeSecondsContinuousPrefab { get;  set; }
		public int MaxContinuousPrefabToSpawn { get;  set; }
		public float MinContinuousScale { get;  set; }
		public float MaxContinuousScale { get;  set; }

	}

}
