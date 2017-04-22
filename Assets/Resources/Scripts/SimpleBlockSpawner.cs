using UnityEngine;
using System.Collections;

public class SimpleBlockSpawner : MonoBehaviour {

	public GameObject SpawnedBlockPrefab;
	public GameObject Player;

	float starttime = 0;
	float duration = 2;
	
	// Use this for initialization
	void Start () {
		starttime = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		if((Time.timeSinceLevelLoad - starttime) > duration)
		{
			SpawnBlock();
			starttime = Time.timeSinceLevelLoad;
		}
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
		go.GetComponent<Rigidbody2D>().velocity = up * 1;
	}
}
