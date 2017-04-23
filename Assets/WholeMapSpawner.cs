using UnityEngine;
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

	private const float BUFFER_SIZE = 10;
	private GameObject _player;

	private enum BufferSide { Left, Right, Up, Down};

	// Use this for initialization
	void Start () {

		_player = GameObject.Find("Player");

		if(_player == null)
		{
			Debug.LogError("unable to find player");
			return;
		}

		if (PrefabOne == null)
			return;

		
        StartCoroutine("SpawnMap");
	}

    // COROUTINE FOR SPEED MOD
    public IEnumerator SpawnMap()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        
        BufferSide currSide;

        for (int i = 0; i < NumberOfItemsToSpawn; ++i)
        {
            currSide = (BufferSide)(i % 4);
            SpawnPrefab(PrefabOne, currSide);
            yield return new WaitForSeconds(.1f);
        }

        yield return null;
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
				location = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, _player.transform.position.y -BUFFER_SIZE), 0);
				break;
		}
		
		var go = Object.Instantiate(prefab);
		go.transform.position = location;
		float randomScale = Random.Range(MinSizePrefabOne, MaxSizePrefabOne);
		go.transform.localScale = new Vector3(randomScale, randomScale, 1);
		float randomSpeed = Random.Range(MinSpeed, MaxSpeed);
		go.GetComponent<Rigidbody2D>().velocity = new Vector3(randomSpeed, randomSpeed, 0);
	}
	

}
