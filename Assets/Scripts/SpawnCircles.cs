using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircles : MonoBehaviour
{
    public GameObject badCircle;
    public GameObject goodCircle;
    public GameObject boundary;
    public List<Vector2> spawnPositions = new List<Vector2>();

    float spawnInterval;
    Vector2 position;
    float halfBoundarySizeX;
    float halfBoundarySizeY;

    void Start()
    {
        GameManager.i.circles = this;
        spawnInterval = GameManager.i.GameMechanicsParameters.basicSpawnInterval;
        halfBoundarySizeX = boundary.GetComponent<BoxCollider2D>().size.x / 2f;
        halfBoundarySizeY = boundary.GetComponent<BoxCollider2D>().size.y / 2f;
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (spawnInterval <= 0.0f)
        {
            CalculatePosition();
            if(Random.Range(0f, 1f) <= GameManager.i.GameMechanicsParameters.chanceForBadCircle)
                CreateCircle(badCircle);
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
        circle.GetComponent<Circle>().SetLifeTime(Random.Range(GameManager.i.GameMechanicsParameters.minLifeTime,
                                        GameManager.i.GameMechanicsParameters.maxLifeTime));
        circle.transform.parent = transform;
    }

    Vector2 RandomPositionWithinBoundary()
    {
        Vector2 temp;
        temp.x = Random.Range(boundary.transform.position.x - halfBoundarySizeX + goodCircle.transform.localScale.x / 2f,
                              boundary.transform.position.x + halfBoundarySizeX - goodCircle.transform.localScale.x / 2f);
        temp.y = Random.Range(boundary.transform.position.y - halfBoundarySizeY + goodCircle.transform.localScale.y / 2f,
                              boundary.transform.position.y + halfBoundarySizeY - goodCircle.transform.localScale.y / 2f);
        return temp;
    }

    void SetSpawnInterval()
    {
        spawnInterval = GameManager.i.GameMechanicsParameters.basicSpawnInterval - GameManager.i.GameMechanicsParameters.spawnIntervalModifier * Time.unscaledTime;
    }
}
