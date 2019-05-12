using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanicsParameters : MonoBehaviour
{
    public float defaultTimeScale;
    public float pausedTimeScale;

    public float basicMinLifeTime;
    public float basicMaxLifeTime;
    public float lifeTimeModifier;
    public float badCircleLifeTime;
    public float fastCircleLifeTime;

    public float basicSpawnInterval;
    public float spawnIntervalModifier;
    public float minSpawnInterval;

    public float chanceForBadCircle;
    public float chanceForFastCircle;
}
