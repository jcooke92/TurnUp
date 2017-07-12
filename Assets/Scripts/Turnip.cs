using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turnip : MonoBehaviour 
{
	RequireComponent Collider2D;
	RequireComponent Animator;
	RequireComponent ParticleSystem;
	RequireComponent SpriteRenderer;

	private int index;
	private const float XOFFSET = 1.125f;
	private const float XMIN = -2.25f;
	private const float YPOS = -3.9f;
	private const float DESTROY_PARTICLE_DURATION = 0.3f;
	private const float SECONDS_UNTIL_DESTROY = 2f;
	private const float RESPAWN_PARTICLE_DURATION = 0.3f;
	private float flashDelay;
	private float explosionDelay;
	private Vector2 moveOffset, origin, positionBuffer;
	private Animator cachedAnimator;
	private ParticleSystem cachedParticleSystem;
	private SpriteRenderer cachedSpriteRenderer;
	private bool isResetting;

	void Start() 
	{
		isResetting = false;
		cachedAnimator = GetComponent<Animator> ();
		cachedSpriteRenderer = GetComponent<SpriteRenderer>();
		cachedParticleSystem = GetComponent<ParticleSystem>();
		
		cachedParticleSystem.Play();
		StartCoroutine(WaitForRespawnParticle());
		StartCoroutine(WaitForFlashDelay());
	}
		
	void Update() 
	{
		if(isResetting)
			lockPosition();
	}

	public void init(float _flashDelay, float _explosionDelay)
	{
		origin = new Vector2 (XMIN + (index * XOFFSET), YPOS);
		resetPosition();
		positionBuffer = transform.position;
		flashDelay = _flashDelay;
		explosionDelay = _explosionDelay;
	}
		
	void OnMouseDown()
	{
		moveOffset = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}

	void OnMouseDrag()
	{
		if(!isResetting)
		{
			Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = cursorPosition + moveOffset;	
			positionBuffer = transform.position;
		}
	}

	void OnMouseUp()
	{
		if(!isResetting)
			resetPosition();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Boundary")
		{
			if (isFlashing()) 
				destroySeqeuence();
			else
				resetGame();
		}
	}

	public void setIndex(int _index)
	{
		index = _index;
	}


	private void resetPosition()
	{
		transform.position = origin;
	}

	private void lockPosition()
	{
		transform.position = positionBuffer;
	}

	private void setFlashing(bool flashing)
	{
		cachedAnimator.SetBool("isFlashing", flashing);
	}

	private bool isFlashing()
	{
		return cachedAnimator.GetBool("isFlashing");
	}

	private void resetGame()
	{
		if(!isResetting)
		{
			GameSaveManager.gameSaveManager.save(LogicManager.logicManager.timePassed);
			SceneManager.LoadScene("GameOver");	
		}
	}

	private void destroySeqeuence()
	{
		isResetting = true;
		cachedSpriteRenderer.enabled = false;
		cachedParticleSystem.Play();
		StartCoroutine(WaitForDestroyParticle());
	}

	IEnumerator WaitForRespawnParticle()
	{
		yield return new WaitForSeconds(RESPAWN_PARTICLE_DURATION);
		cachedParticleSystem.Stop();
	}


	IEnumerator WaitForFlashDelay()
	{
		yield return new WaitForSeconds(flashDelay);
		setFlashing(true);
		StartCoroutine(WaitForExplosionDelay());
	}

	IEnumerator WaitForExplosionDelay()
	{
		yield return new WaitForSeconds(explosionDelay);
		resetGame();
	}

	IEnumerator WaitForDestroyParticle()
	{
		yield return new WaitForSeconds(DESTROY_PARTICLE_DURATION);

		cachedParticleSystem.Stop();
		StartCoroutine(WaitForDestroy());
	}

	IEnumerator WaitForDestroy()
	{
		yield return new WaitForSeconds(SECONDS_UNTIL_DESTROY);
		Destroy(gameObject);
	}
}
