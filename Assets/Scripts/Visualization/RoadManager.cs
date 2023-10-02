using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject car; // Assign the car GameObject
    public GameObject roadTilePrefab; // Assign the road tile prefab
    public Material dryAsphalt; // Assign the dry asphalt material
    
    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    private float tileLength = 5f; // Length of each tile

    void Start()
    {
        // Instantiate initial tiles and set default material
        for (int i = 0; i < 5; i++)
        {
            GameObject newTile = Instantiate(roadTilePrefab, new Vector3(i * tileLength, 0, 0), Quaternion.identity);
            newTile.GetComponent<Renderer>().material = dryAsphalt;
            tileQueue.Enqueue(newTile);
        }
    }

    void Update()
    {
        // Check if the car is over the third tile
        if (car.transform.position.x > (tileQueue.ToArray()[2].transform.position.x))
        {
            MoveTiles();
        }

        // Implement your mechanism for dynamic inclination here...
    }

    void MoveTiles()
    {
        // Dequeue the last tile and destroy it
        GameObject lastTile = tileQueue.Dequeue();
        Destroy(lastTile);

        // Create and enqueue a new tile
        GameObject newTile = Instantiate(roadTilePrefab);
        newTile.transform.position = tileQueue.ToArray()[tileQueue.Count - 1].transform.position + new Vector3(tileLength, 0, 0);
        newTile.GetComponent<Renderer>().material = dryAsphalt; // Set to default material
        tileQueue.Enqueue(newTile);

        // Implement your mechanism to adjust materials and inclination for the new tile here...
    }
}
