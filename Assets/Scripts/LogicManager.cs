using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour 
{
	public static LogicManager logicManager;

	private const int TURNIP_ARRAY_SIZE = 5;
	private const float TURNIP_FLASH_DELAY_MAX = 6f;
	private const float TURNIP_FLASH_DELAY_MIN = 2f;
	private const float TURNIP_EXPLOSION_DELAY_MAX = 8f;
	private const float TURNIP_EXPLOSION_DELAY_MIN = 3f;
	private const float TURNIP_INSTANT_FLASH_MAX_PROB = 0.5f;
	private float turnipSpawnDelay = 1f;
	private int turnipSpawnIndex = -1;
	private int instantFlashProb;
	private GameObject[] turnipArray;
	private ArrayList availableTurnipIndices;

	public float timePassed = 0f;

	void Awake()
	{
		if(logicManager == null)
			logicManager = this;
		else if(logicManager != this)
			Destroy(gameObject);
	}

	void Start () 
	{
		turnipArray = new GameObject[TURNIP_ARRAY_SIZE];
		availableTurnipIndices = new ArrayList();
	}

	void Update () 
	{
		for(int i = 0; i < TURNIP_ARRAY_SIZE; ++i)
		{
			if(turnipArray[i] == null)
				availableTurnipIndices.Add(i);
			else
				availableTurnipIndices.Add(i);
		}

		StartCoroutine(WaitForTurnipRespawn());	

		timePassed += Time.deltaTime;
	}

	void instantiateTurnip(int index, float flashDelay, float explosionDelay)
	{
		if(index >= 0 && index < TURNIP_ARRAY_SIZE && turnipArray[index] == null)
		{
			turnipArray [index] = (GameObject) Instantiate (Resources.Load("Prefabs/turnip"));
			turnipArray [index].GetComponent<Turnip> ().setIndex (index);
			turnipArray [index].GetComponent<Turnip> ().init (flashDelay, explosionDelay);
		}
	}

	IEnumerator WaitForTurnipRespawn()
	{
		yield return new WaitForSeconds(turnipSpawnDelay);

		turnipSpawnIndex = (int) availableTurnipIndices[Random.Range(0, availableTurnipIndices.Count)];
		instantiateTurnip(turnipSpawnIndex, calcFlashDelay(), calcExplosionDelay());
	}

	private float calcFlashDelay()
	{
		float instantFlashProb = 1/(1-(timePassed/100));

		if(instantFlashProb > TURNIP_INSTANT_FLASH_MAX_PROB)
			instantFlashProb = TURNIP_INSTANT_FLASH_MAX_PROB;

		int flashProbMax = (int) Mathf.Round(1/instantFlashProb);

		if(Random.Range(0, flashProbMax) == 0)
			return 0f;

		float delay = TURNIP_FLASH_DELAY_MAX - (timePassed / (20 - Random.Range(0, 10)));

		if(delay < TURNIP_FLASH_DELAY_MIN)
			return TURNIP_FLASH_DELAY_MIN;

		return delay;
	}

	private float calcExplosionDelay()
	{
		float delay = TURNIP_EXPLOSION_DELAY_MAX - (timePassed / 20);

		if(delay < TURNIP_EXPLOSION_DELAY_MIN)
			return TURNIP_EXPLOSION_DELAY_MIN;

		return delay;
	}
}