using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsDamageEnemy : MonoBehaviour {

	public int damage = 9999;

	void OnCollisionEnter2D(Collision2D _colInfo)
	{

		Enemy _enemy = _colInfo.collider.GetComponent<Enemy>();

		if(_enemy != null)
		{
			_enemy.DamageEnemy(damage);
		}
	}
}
