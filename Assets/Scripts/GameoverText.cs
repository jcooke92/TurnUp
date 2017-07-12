using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverText : MonoBehaviour 
{
	RequireComponent SpriteRenderer;

	private const float FADE_SPEED = 0.15f;

	private float redChannel;
	private float greenChannel;
	private float blueChannel;
	private float alphaChannel = 0.0f;

	private SpriteRenderer cachedSpriteRenderer;

	void Start () 
	{
		cachedSpriteRenderer = GetComponent<SpriteRenderer>();
		redChannel = cachedSpriteRenderer.color.r;
		greenChannel = cachedSpriteRenderer.color.g;
		blueChannel = cachedSpriteRenderer.color.b;
	}
	

	void Update () 
	{
		
	}

	void OnGUI()
	{
		cachedSpriteRenderer.color = new Color(redChannel, greenChannel, blueChannel, alphaChannel);
		alphaChannel += FADE_SPEED * Time.deltaTime;
	}
}
