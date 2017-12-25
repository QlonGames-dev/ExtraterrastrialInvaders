using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsDamage : MonoBehaviour {

	public int damage = 9999;


	void OnCollisionEnter2D(Collision2D _colInfo)
	{
		Player _player = _colInfo.collider.GetComponent<Player>();

		if(_player != null)
		{
			_player.DamagePlayer(damage);
		}

	}
}
