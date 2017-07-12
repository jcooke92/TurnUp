using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMedal : MonoBehaviour 
{
	RequireComponent SpriteRenderer;

	private SpriteRenderer cachedSpriteRenderer;

	int spriteIndex = 10;

	void Start () 
	{
		cachedSpriteRenderer = GetComponent<SpriteRenderer>();
		spriteIndex = GameSaveManager.gameSaveManager.currentMedal;

		Sprite[] sprites = Resources.LoadAll<Sprite>("UI/MedalSheet");
		cachedSpriteRenderer.sprite = sprites[spriteIndex];
	}
}
