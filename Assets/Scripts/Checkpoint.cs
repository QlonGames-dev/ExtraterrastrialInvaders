using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public enum state {Active, Inactive, Used, Locked};

	public state status;

	public checkpointHandler ch;

	public Sprite[] sprites;

	void Start()
	{
		ch = GameObject.Find ("CheckpointHandler").GetComponent<checkpointHandler> ();
	}

	void Update()
	{
		ChangeColor ();
	}
	
	// Update is called once per frame
	void ChangeColor () 
	{
		if (status == state.Inactive) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [0];
		} 
		else if(status == state.Active)
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [1];
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))//other.tag == "Player" 
		{
			GameMaster.playerPosition = other.transform.position;
			ch.UpdateCheckpoint (this.gameObject);
		}
	}
}
