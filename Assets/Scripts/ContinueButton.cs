using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour 
{
	RequireComponent Button;

	public const float EXPLOSION_ANIM_DELAY = 2f;

	private Button cachedButton;

	void Start () 
	{
		cachedButton = GetComponent<Button>();
		cachedButton.enabled = false;
		StartCoroutine(WaitForExplosionAnim());
	}

	void Update () 
	{

	}

	IEnumerator WaitForExplosionAnim()
	{
		yield return new WaitForSeconds(EXPLOSION_ANIM_DELAY);
		cachedButton.enabled = true;
	}
}
