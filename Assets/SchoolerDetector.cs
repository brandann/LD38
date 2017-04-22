using UnityEngine;
using System.Collections;

public class SchoolerDetector : MonoBehaviour {
	public GameObject ParentSchoolerGo;
	private Schooler _parentSchooler;

	// Use this for initialization
	void Start () {
		if (ParentSchoolerGo)
		{
			_parentSchooler = ParentSchoolerGo.GetComponent<Schooler>();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{	
		if(_parentSchooler != null)
			_parentSchooler.HandleCollisionFromDetector(other);	
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (_parentSchooler != null)
			_parentSchooler.HandleRemoveCollisionFromDetector(other);

	}
}
