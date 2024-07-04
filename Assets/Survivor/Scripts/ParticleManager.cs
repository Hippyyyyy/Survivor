using SCN.Common;
using SCN.Effect;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle manager", menuName = "Particle/Particle manager")]
public class ParticleManager : ScriptableObject
{
    private static ParticleManager ins;

    public static ParticleManager Ins
    {
        get => ins ??= LoadSource.LoadObject<ParticleManager>("Particle manager");
    }

    [SerializeField] GameObject enemyDie;

    public ParticleObj EnemyDie(Transform parent)
    {
        var obj = ParticleSystemCallMaster.Instance.PlayParticle(parent, enemyDie);
        return obj;
    }
}

