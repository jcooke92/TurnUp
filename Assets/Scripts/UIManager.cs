using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	public void loadMainScene()
	{
		SceneManager.LoadScene ("Main");
	}

	public void loadHowToScene()
	{
		SceneManager.LoadScene ("Directions");
	}

	public void loadScoreScene()
	{
		SceneManager.LoadScene ("Score");
	}

	public void loadMenuScene()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void openURL()
	{
		Application.OpenURL("http://www.jonathanandrescooke.com");
	}
}
