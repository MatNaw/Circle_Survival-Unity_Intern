using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Circle : MonoBehaviour
{
    public Image fillImage;

    float lifeTime;
    float lifeTimeLeft;

    void Update()
    {
        countdownToDie();
    }

    void countdownToDie()
    {
        if (lifeTimeLeft > 0.0f)
        {
            lifeTimeLeft -= Time.deltaTime;
            if (fillImage != null) fillImage.fillAmount += 1.0f / lifeTime * Time.deltaTime;
        }
        else
        {
            DestroyCircle();
            if (gameObject.tag != "BadCircle") GameManager.i.GameOver();
        }
    }

    public void SetLifeTime(float time)
    {
        lifeTime = lifeTimeLeft = time;
    }

    public void DestroyCircle()
    {
        GameManager.i.circles.spawnPositions.Remove(this.transform.position);
        GameManager.i.circles.circlesList.Remove(this);
        Destroy(gameObject);
    }
}
