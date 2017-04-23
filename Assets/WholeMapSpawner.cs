﻿using UnityEngine;
using System.Collections;

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
	public float DistanceFromPlayer;
	public float MinContinuousScale;
	public float MaxContinuousScale;
	private int _totalContinousPrefabsSpawned;
	
	#endregion

	private const float BUFFER_SIZE = 10;
	private GameObject _player;

	private enum BufferSide { Left, Right, Up, Down };

	// Use this for initialization
	void Start() {

		_player = GameObject.Find("Player");

		if (_player == null)
		{
			Debug.LogError("unable to find player");
			return;
		}

		if (PrefabOne == null)
			return;


		//StartCoroutine("SpawnInitialMap");
		PopulateInitialWorldAtOnce();
		StartCoroutine("SpawnContinuous");
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
			GameObject go = SpawnPrefabAwayFromPlayer(ContinuousPrefab, DistanceFromPlayer);
			GiveRandomSpeed(go);
			GiveRandomScale(go, MinContinuousScale, MaxContinuousScale);
			_totalContinousPrefabsSpawned++;

		}
		yield return null;
	}

	// COROUTINE FOR SPEED MOD
	public IEnumerator SpawnInitialMap()
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


}
