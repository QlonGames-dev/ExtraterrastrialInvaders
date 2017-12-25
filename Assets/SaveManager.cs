using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Save
{
	public int health;
	public int coin;
	public float x, y, z;
}

public class SaveManager : MonoBehaviour 
{

	public int health;
	public int coin;

	void FixedUpdate()
	{
		SaveParams ();
	}

	public void SaveParams()
	{
		var xml = new XmlSerializer (typeof(Save));

		var save = new Save ();

		save.health = health;
		save.coin = coin;

		save.x = transform.position.x;
		save.y = transform.position.y;

		using (var stream = new FileStream ("Save.xml", FileMode.Create, FileAccess.Write)) 
		{
			xml.Serialize (stream, save);
		}
	}

	public void LoadParams()
	{
		var xml = new XmlSerializer (typeof(Save));

		var save = new Save ();

		using (var stream = new FileStream ("Save.xml", FileMode.Open, FileAccess.Read)) 
		{
			save = xml.Deserialize (stream) as Save;
		}

		health = save.health;
		coin = save.coin;
		transform.position = new Vector2 (save.x, save.y);

	}
}
