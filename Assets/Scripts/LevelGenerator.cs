using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator instance;

    public GameObject point;
    public GameObject[] bonuses;


    public GameObject upperLine;
    public GameObject lowerLine;
    

    public int numberOfPoint;
    public int numberOfBonuses;
    public float levelWidth = 2.5f;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        createPoints();
        moveLines();
    }

    public void createPoints()
    {
        /*
        Vector3 spawnPosition = new Vector3();

        for(int i = 0;i < numberOfPoint; i++)
        {
            spawnPosition.y = Random.Range(lowerLine.transform.position.y, upperLine.transform.position.y);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(point, spawnPosition, Quaternion.identity);
        }

        for(int i = 0; i < numberOfBonuses; i++)
        {
            spawnPosition.y = Random.Range(lowerLine.transform.position.y, upperLine.transform.position.y);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(bonuses[Random.Range(0, bonuses.Length)], spawnPosition, Quaternion.identity);
        }
        */

        Vector3 spawnPosition = new Vector3();
        spawnPosition.y = lowerLine.transform.position.y;
        
        while (true)
        {
            spawnPosition.y += Random.Range(0.5f,4);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(point, spawnPosition, Quaternion.identity);

            if (spawnPosition.y > upperLine.transform.position.y) {
                break;
            }
        }
        createBonus();
    }

    public void createBonus()
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.y = Random.Range(lowerLine.transform.position.y, upperLine.transform.position.y);
        spawnPosition.x = Random.Range(-levelWidth, levelWidth);
        Instantiate(bonuses[Random.Range(0, bonuses.Length)], spawnPosition, Quaternion.identity);
    }

    public void moveLines()
    {
        upperLine.transform.position = new Vector3(0, upperLine.transform.position.y + 30, 0);
        lowerLine.transform.position = new Vector3(0, lowerLine.transform.position.y + 30, 0);
        createPoints();
    }
}
