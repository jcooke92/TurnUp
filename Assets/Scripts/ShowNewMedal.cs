using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNewMedal : MonoBehaviour 
{

	RequireComponent SpriteRenderer;

	SpriteRenderer cachedSpriteRenderer;

	void Start () 
	{
		cachedSpriteRenderer = GetComponent<SpriteRenderer>();
		cachedSpriteRenderer.enabled = GameSaveManager.gameSaveManager.newMedal;
		GameSaveManager.gameSaveManager.newMedal = false;
	}

	void Update () 
	{
		
	}
}
