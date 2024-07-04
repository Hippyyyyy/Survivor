using SCN.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	Rigidbody2D rb;

    [SerializeField]
	SpriteRenderer BrickSprite;

	[NonSerialized]
	public int BrickIndex;

	[NonSerialized]
	public bool IsPauseGame;

	[SerializeField]
	BoxCollider2D hitCollider;

	[SerializeField]
	LayerMask enemyMask;

	List<EnemyBase> damagedEnemies = new List<EnemyBase>();

	ObjectPool brickPool;

	float FlyingTime;

	float TargetX;

	float TargetY1;

	float TargetY2;

    private void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
		hitCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {

	}

    private void Update()
    {
		if (transform.position.y <= -10)
		{
			RemoveObj();
		}
		AOE(5);
	}
    private void OnEnable()
    {
		hitCollider.enabled = true; 
    }
    public void SetFlying(float velocityMultiplier, Vector3 scaleSizeMultiplier, int duration)
	{
		FlyingTime = duration;
		rb.velocity = new Vector3(rb.velocity.x, 0);
		transform.localScale = scaleSizeMultiplier;
		rb.AddForce(Vector3.up * velocityMultiplier, ForceMode2D.Impulse);
		float randomX = UnityEngine.Random.Range(-1f, 1f);
		rb.AddForce(new Vector2(randomX * 1.5f, 0), ForceMode2D.Impulse);
	}

	public void AOE(float damage)
    {
		Collider2D[] enemies = Physics2D.OverlapBoxAll(
			transform.position,
			hitCollider.bounds.size,
			transform.localEulerAngles.z,
			enemyMask);
		for (int i = 0; i < enemies.Length; i++)
		{
			EnemyBase enemy = enemies[i].GetComponent<EnemyBase>();
			if (!damagedEnemies.Contains(enemy))
			{
				enemy.TakeDamage(damage, false);
				damagedEnemies.Add(enemy);
			}
		}
	}
	public void SetPool(ObjectPool pool)
	{
		this.brickPool = pool;
	}
	public void RemoveObj()
	{
		gameObject.SetActive(false);
		hitCollider.enabled = false;
		brickPool.RemoveObj(gameObject);
	}
}
