using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaveManager : MonoBehaviour 
{

	public static GameSaveManager gameSaveManager;

	public float currentHighScore = 0f;

	public bool newMedal = false;

	public int currentMedal = 10;

	void Awake()
	{
		if(gameSaveManager == null)
		{
			DontDestroyOnLoad(gameObject);
			gameSaveManager = this;	
		}
		else if(gameSaveManager != this)
		{
			Destroy(gameObject);	
		}
	}

	void Start () 
	{
		load();	
	}

	void Update () 
	{
		
	}

	public void save(float timePassed)
	{
		if(timePassed > currentHighScore)
		{
			currentHighScore = timePassed;

			calcCurrentMedal();

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream saveFile = File.Open(Application.persistentDataPath + "/score.dat", FileMode.Open);

			SaveData data = new SaveData(timePassed, currentMedal);

			formatter.Serialize(saveFile, data);

			saveFile.Close();
		}
	}

	public void load()
	{
		if(!File.Exists(Application.persistentDataPath + "/score.dat"))
			File.Create(Application.persistentDataPath + "/score.dat");

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open(Application.persistentDataPath + "/score.dat", FileMode.Open);

		SaveData data = (SaveData) formatter.Deserialize(saveFile);

		currentHighScore = data.score;
		currentMedal = data.medal;

		saveFile.Close();

		calcCurrentMedal();
	}

	public void calcCurrentMedal()
	{
		int oldMedal = currentMedal;
		if(currentHighScore < 20f)
			currentMedal = 10;
		else
		{
			currentMedal = (int) Mathf.Floor(currentHighScore / 20) - 1;
			if(currentMedal > 9)
				currentMedal = 9;
		}

		if(oldMedal > currentMedal && oldMedal == 10)
			newMedal = true;
		else if(oldMedal < currentMedal)
			newMedal = true;
	}
}

[System.Serializable]
class SaveData
{
	public float score;

	public int medal;

	public SaveData(float time, int _medal)
	{
		score = time;
		medal = _medal;
	}
}
