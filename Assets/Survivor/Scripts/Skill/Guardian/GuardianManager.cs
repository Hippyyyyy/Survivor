using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianManager : BaseActiveSkill
{
    [SerializeField]
    AudioClip TriggerSound;

    [SerializeField]
    int[] SawCountArray;

    [SerializeField]
    Guardian[] SawObjects;

    [SerializeField]
    AutoRotate SelfRotate;

    [SerializeField]
    private Sprite GuardianEvoSprite;

   float PassiveAOE;
   float PassiveSpeed;
   float PassiveReduceCD;
   float PassiveDuration;
   int SkillDuration;

    [SerializeField]
    private int ScaleDownTime;

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

    public void SetDuration(float newValue)
    {
        PassiveDuration = newValue;
    }

    private void ResetPositionForSaws()
    {
        
    }

    private void RotateVector(ref Vector3 inputVector, float angle)
    {
        float sinAngle = Mathf.Sin(angle);
        float cosAngle = Mathf.Cos(angle);
        float x = inputVector.x * cosAngle - inputVector.y * sinAngle;
        float y = inputVector.x * sinAngle + inputVector.y * cosAngle;
        inputVector.x = x;
        inputVector.y = y;
    }

    protected override void TriggerSkill()
    {
        /*if (TriggerSound != null)
        {
            AudioSource.PlayClipAtPoint(TriggerSound, transform.position);
        }

        float angleStep = 360f / SawObjects.Length;
        for (int i = 0; i < SawObjects.Length; i++)
        {
            Vector3 sawPosition = SawObjects[i].transform.localPosition;
            RotateVector(ref sawPosition, angleStep * i * Mathf.Deg2Rad);
            SawObjects[i].transform.localPosition = sawPosition;
        }*/
        
    }

    void DamageArea(Vector2 position, float radius, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D item in targets)
        {
            EnemyBase enemy = item.GetComponent<EnemyBase>();
            if (enemy)
                enemy.TakeDamage(damage, false);
        }
    }

    protected override void Update()
    {
        for (int i = 0; i < SawObjects.Length; i++)
        {
            DamageArea(SawObjects[i].transform.position, SawObjects[i].CircleCollider2d.radius, PassiveAOE);
        }
    }
   
}
