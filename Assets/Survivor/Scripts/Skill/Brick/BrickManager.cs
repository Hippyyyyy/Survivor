using SCN.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : BaseActiveSkill
{
	[SerializeField]
	private AudioClip ShootSound_Normal;

	[SerializeField]
	private AudioClip ShootSound_Evo;

	[SerializeField]
	int[] BrickCountArray;

	int MaxBrickCount;

	float PassiveAOE;

	float PassiveSpeed;

	float PassiveReduceCD;

	[SerializeField]
	private Brick BrickPrefab;

	[SerializeField]
	Transform BrickContainer;

	List<Brick> BrickList;

	Player player;

	ObjectPool brickPool;

	int currentSkillLevel = 0;

	protected override void Awake()
    {
        base.Awake();
		PassiveSpeed = 20;
		BrickList = new List<Brick>();
		brickPool = new ObjectPool(BrickPrefab.gameObject, BrickContainer);
		player = FindFirstObjectByType<Player>();
    }

    private void Start()
    {
		StartCoroutine(ThrowBrickCoroutine());
	}

    protected override void Update()
    {
        base.Update();
        /*for (int i = 0; i < BrickList.Count; i++)
        {
			var brick = BrickList[i];
            if (brick.enabled)
            {
				brick.AOE(damageMultiplier[currentSkillLevel]);
			}
			
		}*/
    }

    protected override void DealDamage()
	{
	}

	public void SetAOE(float newValue)
	{
		PassiveAOE = newValue;
	}

	public void SetBulletSpeed(float newValue)
	{
		PassiveSpeed = newValue;
	}

	public void SetCooldownReduction(float newValue)
	{
		PassiveReduceCD = newValue;
	}

	protected override void SetCooldown()
	{
	}

	protected override void TriggerSkill()
	{
	}

	private IEnumerator ThrowBrickCoroutine()
	{
		while (true)
		{
			ThrowBrick();
			yield return new WaitForSeconds(2f);
		}
	}

	void ThrowBrick()
	{
		int Count = numberOfProjectiles[currentSkillLevel];
		for (int i = 0; i < Count; i++)
		{
			GameObject brickObj = brickPool.GetObjInPool();
			Brick brick = brickObj.GetComponent<Brick>();
			brick.SetPool(brickPool);
			brick.transform.position = player.transform.position;
			brick.gameObject.SetActive(true);
			brick.SetFlying(PassiveSpeed, Vector3.one * 0.7f, 1);
			//brick.AOE(damageMultiplier[currentSkillLevel]);
			BrickList.Add(brick);
		}
	}

}
