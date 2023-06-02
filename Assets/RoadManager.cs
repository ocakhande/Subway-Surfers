using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public float zSpawn = 0;
    public float roadLength = 30;
    public int numberOfRoads = 6;
    private List<Transform> activeRoadList = new List<Transform>();
    public Transform playerTransform;

    private float playerOffset;

    private void Start()
    {
        GenerateInitialRoads();
        playerOffset = playerTransform.position.z - transform.position.z;
    }

    private void GenerateInitialRoads()
    {
        for (int i = 0; i < numberOfRoads; i++)
        {
            SpawnRoad();
        }
    }

    private void Update()
    {
        float playerZPosition = playerTransform.position.z - playerOffset;
        if (playerZPosition > zSpawn - (numberOfRoads * roadLength))
        {
            SpawnRoad();
            DeleteRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject randomRoadPrefab = roadPrefabs[Random.Range(0, roadPrefabs.Length)];
        GameObject road = Instantiate(randomRoadPrefab, transform.position + new Vector3(0, 0, zSpawn), Quaternion.identity);
        road.transform.SetParent(transform);
        zSpawn += roadLength;
        activeRoadList.Add(road.transform);
    }

    private void DeleteRoad()
    {
        Transform oldestRoad = activeRoadList[0];
        activeRoadList.Remove(oldestRoad);
        Destroy(oldestRoad.gameObject);
    }
}












//public void randRoad()
//{
//    SpawnRoad(Random.Range(0, ));
//}








//void Start()
//{
//    for(int i = 0; i < numberOfRoads; i++)
//    {
//        if(i==0)
//        {
//            SpawnRoad(0);
//        }
//        else
//        SpawnRoad(Random.Range(0,roadPrefabs.Length));
//    }

//}

//// Update is called once per frame
//void Update()
//{
//    if(playerTransform.transform.position.z-30 > zSpawn-(numberOfRoads * roadLength))
//    {
//        SpawnRoad(Random.Range(0, roadPrefabs.Length));
//        DeleteRoad();
//    }
//}

//public void SpawnRoad ( int roadIndex)
//{
//    GameObject road= Instantiate(roadPrefabs[roadIndex], transform.forward * zSpawn, transform.rotation);
//    zSpawn += roadLength;
//}
//private void DeleteRoad()
//{
//    Destroy(roadList[0]);
//    roadList.RemoveAt(0);
//}

//}
