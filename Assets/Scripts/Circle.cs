using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
        {
            GameManager.i.circles.spawnPositions.Remove(this.transform.position);
            Destroy(gameObject);
        }
    }

    public void SetLifeTime(float time)
    {
        lifeTime = time;
    }
}
