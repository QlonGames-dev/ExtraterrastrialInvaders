using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

	void OnTriggerEnter(Collider tr)
	{
		if (tr.transform.CompareTag ("Player")) 
		{
			GameMaster.playerPosition = tr.transform.position;
		}	
	}
}
