using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillManager : BaseActiveSkill
{
	[SerializeField]
	private AudioClip ShootSound_Normal;

	/*[SerializeField]
	private WhistlingArrow WhistlingArrow;*/

	[Tooltip("Số drill mỗi lần bắn tùy vào grade của skill")]
	[SerializeField]
	private int[] DrillCountArray;

	private float PassiveAOE;

	private float PassiveSpeed;

	private float PassiveReduceCD;

	private float PassiveDuration;

	[SerializeField]
	private Drill DrillPrefab;

	[SerializeField]
	private Transform DrillContainer;

	private List<Drill> DrillList;

	protected override void DealDamage()
	{
	}

	public void SetAOE(float newValue)
	{
	}

	public void SetBulletSpeed(float newValue)
	{
	}

	public void SetCooldownReduction(float newValue)
	{
	}

	protected override void SetCooldown()
	{
	}

	public void SetDuration(float newValue)
	{
	}


	protected override void TriggerSkill()
	{
	}

	private void ShootDrill()
	{
	}

	private void DespawnDrill()
	{
	}

}
