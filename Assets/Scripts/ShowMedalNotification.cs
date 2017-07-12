using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMedalNotification : MonoBehaviour 
{
	RequireComponent SpriteRenderer;

	SpriteRenderer cachedSpriteRenderer;

	void Start () 
	{
		cachedSpriteRenderer = GetComponent<SpriteRenderer>();
		cachedSpriteRenderer.enabled = GameSaveManager.gameSaveManager.newMedal;
	}

	void Update () 
	{
		
	}
}
