using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuningEnemy : MonoBehaviour {

    public float speed = 7.0f;
    float direction = -1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed * direction, GetComponent<Rigidbody2D>().velocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
            direction *= -1.0f;
    }
}