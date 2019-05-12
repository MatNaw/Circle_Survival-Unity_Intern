using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircles : MonoBehaviour
{
    public GameObject badCircle;
    public GameObject goodCircle;
    public GameObject fastCircle;
    public GameObject boundary;
    public List<Vector2> spawnPositions = new List<Vector2>();
    public List<Circle> circlesList = new List<Circle>();

    float spawnInterval;
    float minLifeTime;
    float maxLifeTime;

    Vector2 position;
    float minSpawnX;
    float maxSpawnX;
    float minSpawnY;
    float maxSpawnY;
    int maxNumberOfCircles;

    void Start()
    {
        GameManager.i.circles = this;
        GameManager.i.restart += Reset;
        SetSpawnBounds();
        SetMaxSpawnedCircles();
    }

    void Update()
    {
        if(GameManager.i.isGameRunning && circlesList.Count < maxNumberOfCircles)
            Spawn();
    }

    void Spawn()
    {
        if (spawnInterval <= 0.0f || circlesList.Count == 0)
        {
            CalculatePosition();
            float random = Random.Range(0f, 1f);
            if (random <= GameManager.i.GameMechanicsParameters.chanceForBadCircle)
            {
                CreateCircle(badCircle);
            }
            else if (random <= GameManager.i.GameMechanicsParameters.chanceForBadCircle + GameManager.i.GameMechanicsParameters.chanceForFastCircle)
            {
                CreateCircle(fastCircle);
            }
            else CreateCircle(goodCircle);
            SetSpawnInterval();
        }
        else
        {
            spawnInterval -= Time.deltaTime;
        }
    }

    void CalculatePosition()
    {
        do
        {
            position = RandomPositionWithinBoundary();
        } while (isCollidingWithOtherCircles());
        spawnPositions.Add(position);
    }

    bool isCollidingWithOtherCircles()
    {
        foreach (Vector2 pos in spawnPositions)
        {
            if ((pos - position).magnitude < goodCircle.transform.localScale.x) return true;
        }
        return false;
    }

    void CreateCircle(GameObject obj)
    {
        GameObject circle = Instantiate(obj, position, Quaternion.identity);
        if (obj.tag == "FastCircle")
        {
            circle.GetComponent<Circle>().SetLifeTime(GameManager.i.GameMechanicsParameters.fastCircleLifeTime);
        }
        if (obj.tag == "BadCircle")
        {
            circle.GetComponent<Circle>().SetLifeTime(GameManager.i.GameMechanicsParameters.badCircleLifeTime);
        }
        else
        {
            circle.GetComponent<Circle>().SetLifeTime(Random.Range(minLifeTime - GameManager.i.GameMechanicsParameters.lifeTimeModifier * Time.unscaledDeltaTime,
                                                                   maxLifeTime - GameManager.i.GameMechanicsParameters.lifeTimeModifier * Time.unscaledDeltaTime
                                                                  )
                                                     );
        }
        circle.transform.parent = transform;
        circlesList.Add(circle.GetComponent<Circle>());
    }

    Vector2 RandomPositionWithinBoundary()
    {
        Vector2 temp;
        temp.x = Random.Range(minSpawnX, maxSpawnX);
        temp.y = Random.Range(minSpawnY, maxSpawnY);
        return temp;
    }

    void SetSpawnBounds()
    {
        float halfBoundarySizeX = boundary.GetComponent<BoxCollider2D>().size.x / 2f;
        float halfBoundarySizeY = boundary.GetComponent<BoxCollider2D>().size.y / 2f;

        minSpawnX = boundary.transform.position.x - halfBoundarySizeX + (goodCircle.transform.localScale.x / 2f);
        maxSpawnX = boundary.transform.position.x + halfBoundarySizeX - (goodCircle.transform.localScale.x / 2f);

        minSpawnY = boundary.transform.position.y - halfBoundarySizeY + (goodCircle.transform.localScale.y / 2f);
        maxSpawnY = boundary.transform.position.y + halfBoundarySizeY - (goodCircle.transform.localScale.y / 2f);
    }

    void SetMaxSpawnedCircles()
    {
        maxNumberOfCircles = (int) ((maxSpawnX - minSpawnX) * (maxSpawnY - minSpawnY) / (goodCircle.transform.localScale.x * 1.75f));
    }

    void SetSpawnInterval()
    {
        if (spawnInterval > GameManager.i.GameMechanicsParameters.minSpawnInterval)
        {
            spawnInterval = GameManager.i.GameMechanicsParameters.basicSpawnInterval - GameManager.i.GameMechanicsParameters.spawnIntervalModifier * Time.unscaledTime;
        }
        else spawnInterval = GameManager.i.GameMechanicsParameters.minSpawnInterval;
    }

    void Reset()
    {
        for (int i = circlesList.Count - 1; i >= 0; i--)
        {
            circlesList[i].DestroyCircle();
        }

        circlesList = new List<Circle>();
        spawnInterval = GameManager.i.GameMechanicsParameters.basicSpawnInterval;
        minLifeTime = GameManager.i.GameMechanicsParameters.basicMinLifeTime;
        maxLifeTime = GameManager.i.GameMechanicsParameters.basicMaxLifeTime;
    }
}
