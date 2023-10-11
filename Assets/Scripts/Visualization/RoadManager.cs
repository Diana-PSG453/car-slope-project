using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject car; // Assign the car GameObject
    public GameObject roadTilePrefab; // Assign the road tile prefab
    public Material dryAsphalt; // Assign the dry asphalt material
    
    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    private float tileLength = 5f; // Length of each tile
    [SerializeField] private float inclinationAngle = 0f; // Inclination angle for new tiles

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
    Vector3 lastTilePosition = tileQueue.ToArray()[tileQueue.Count - 1].transform.position;
    
    // Adjust the height (Y-axis) and depth (X-axis) based on the inclination angle
    float deltaY = tileLength * Mathf.Sin(Mathf.Deg2Rad * inclinationAngle); // Height difference due to inclination
    float deltaX = tileLength * Mathf.Cos(Mathf.Deg2Rad * inclinationAngle); // Depth difference due to inclination

    // Set the new tile's position to connect seamlessly with the last tile
    newTile.transform.position = new Vector3(lastTilePosition.x + deltaX, lastTilePosition.y + deltaY, lastTilePosition.z);
    newTile.GetComponent<Renderer>().material = dryAsphalt; // Set to default material
    SetInclination(newTile); // Set inclination
    tileQueue.Enqueue(newTile);

    // Implement your mechanism to adjust materials for the new tile here...
}

// Function to set inclination angle
void SetInclination(GameObject tile)
{
    tile.transform.Rotate(Vector3.forward * inclinationAngle);
}

}