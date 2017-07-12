using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour 
{
	RequireComponent Text;

	private Text textComponent;

	void Start () 
	{
		textComponent = GetComponent<Text>();
		textComponent.text = "" + LogicManager.logicManager.timePassed;
	}

	void Update () 
	{
		textComponent.text = string.Format("{0:0.0}", LogicManager.logicManager.timePassed);
	}
}
