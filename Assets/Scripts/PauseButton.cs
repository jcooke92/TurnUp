using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour 
{
	RequireComponent Image;

	GameObject pauseText;
	Image cachedImage;

	Sprite pauseSprite, resumeSprite;

	bool paused = false;

	void Start () 
	{
		cachedImage= GetComponent<Image>();
		pauseSprite = Resources.Load<Sprite>("UI/pausebutton2");
		resumeSprite = Resources.Load<Sprite>("UI/resumebutton2");
	}

	void Update () 
	{
		
	}

	public void pause()
	{
		if(paused)
			resume();
		else
		{
			Time.timeScale = 0;
			cachedImage.sprite = resumeSprite;
			pauseText = (GameObject) Instantiate (Resources.Load("Prefabs/PauseText"));
			paused = true;
		}
	}

	public void resume()
	{
		Destroy(pauseText.gameObject);
		cachedImage.sprite = pauseSprite;
		Time.timeScale = 1;
		paused = false;
	}
}
