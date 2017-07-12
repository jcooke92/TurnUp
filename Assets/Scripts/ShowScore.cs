using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour 
{

	RequireComponent Text;

	private Text textComponent;

	void Start () 
	{
		textComponent = GetComponent<Text>();
		textComponent.text = string.Format("{0:0.0}", GameSaveManager.gameSaveManager.currentHighScore);
	}
}
