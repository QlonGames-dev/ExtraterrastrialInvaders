using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform watchStart, watchEnd;

    public bool spotted = false;

    void Update()
    {
        Raycasting();
    }

    void Raycasting()
    {
        Debug.DrawLine(watchStart.position, watchEnd.position, Color.red);
        spotted = Physics2D.Linecast(watchStart.position, watchEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }
}
