using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
	[SerializeField]
	private Transform MeteorTransform;

	[SerializeField]
	private SpriteRenderer MeteorSpriteRenderer;

	[SerializeField]
	private SpriteRenderer ExplosionSpriteRenderer;

	[NonSerialized]
	public int MeteorIndex;

	[NonSerialized]
	public bool IsPauseGame;

	Vector3 CurrentVelocity;

	float FlyTime;

	float Timer;

	MeteorFallManager MeteorFallManager;

	public void Shoot(Vector2 velocity, float flyTime, Vector2 aoeMultiplier, MeteorFallManager meteorFallManager)
	{
		CurrentVelocity = velocity;
		FlyTime = flyTime;
		Timer = 0;
		MeteorTransform.localScale = new Vector3(aoeMultiplier.x, aoeMultiplier.y, 1);
		MeteorFallManager = meteorFallManager;
	}

	private void DealExplosionDamage()
	{
		ExplosionSpriteRenderer.enabled = true;
		MeteorSpriteRenderer.enabled = false;
	}

	public void Update()
	{
		if (IsPauseGame) return;

		Timer += Time.time;
		if (Timer >= FlyTime)
		{
			DealExplosionDamage();
		}
		else
		{
			MeteorTransform.position += CurrentVelocity * Time.deltaTime;
		}
	}

	private void SetMeteorDirection()
	{

	}
}
