using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulateWorld : MonoBehaviour {
    
    public int NumberOfTriangles;
    private List<GameObject> _triangle;
    public GameObject Player;
    public GameObject TrianglePrefab;
    private float _Offset = 1;
    public float XOffSetFactor = 0.8f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CreateTriangle();
        //if (Input.GetKeyDown(KeyCode.W))
           
    }

    void CreateTriangle()
    {
        _Offset += XOffSetFactor;
        float _xOffset = XOffSetFactor * 1.0f + (float)(Random.Range(1, 100) / 100f);
        float _yOffset = XOffSetFactor * 1.0f + (float)(Random.Range(1, 100 - _xOffset) / 100f);
        
        
         _triangle = new List<GameObject>(NumberOfTriangles);
        var tri = Object.Instantiate(TrianglePrefab);
        tri.transform.position = Player.transform.position + new Vector3(_xOffset,_yOffset,0);
        
        print("Create Triangle with offset: " + _Offset.ToString());
    }


	// Use this for initialization
	void Start () {
       
        for (int i = 0; i < NumberOfTriangles; ++i)
        {

        }
	
	}
	
	
}
