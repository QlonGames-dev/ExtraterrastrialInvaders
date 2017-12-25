using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : MonoBehaviour 
{

    public static GameMaster gm;
	public int spawnDelay = 2;
	public static Vector3 playerPosition;
	public Transform playerPrefab;
	public Vector3 offset;

	private SaveGame saveGame = new SaveGame ();

    void Awake()
    {
		if (PlayerPrefs.HasKey ("saveGame")) 
		{
			saveGame = JsonUtility.FromJson<SaveGame> (PlayerPrefs.GetString ("saveGame"));
			ScoreManager.score = saveGame.scorePoint;
			Debug.Log (ScoreManager.score);	
			playerPosition = saveGame.playerPos;
			Debug.Log (playerPosition);
		}  
		else 
		{
			Debug.Log ("Don't load saving game.");	
		}

		Instantiate (playerPrefab, playerPosition + offset, Quaternion.identity); //Создание игрока

        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }
		
	[SerializeField]
	private AudioManager audioManager;

	void Start()
	{
		
		//cashing
		audioManager = AudioManager.instance;
		if (audioManager == null) 
		{
			Debug.LogError ("No Audio Manager found in the scene.");
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public IEnumerator RespawnPlayer()
	{
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, playerPosition + offset, Quaternion.identity);
	}

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
		gm.StartCoroutine(gm.RespawnPlayer ());
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

	private void OnApplicationQuit()
	{
		saveGame.scorePoint = ScoreManager.score;
		saveGame.playerPos = playerPosition;
		PlayerPrefs.SetString("saveGame", JsonUtility.ToJson (saveGame));
	}
}

[Serializable]
public class SaveGame
{
	public int scorePoint;
	public Vector3 playerPos;
}