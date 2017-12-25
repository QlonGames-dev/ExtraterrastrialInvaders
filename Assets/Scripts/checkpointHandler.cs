using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointHandler : MonoBehaviour {

	public GameObject[] checkpoints;

	// Use this for initialization
	void Start () {
		checkpoints = GameObject.FindGameObjectsWithTag ("checkpoint");
	}

	public void UpdateCheckpoint(GameObject curCheck)
	{
		curCheck.GetComponent<Checkpoint> ().status = Checkpoint.state.Active;
	}
}
