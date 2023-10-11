using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private enum RoadType {DryAsphalt, WetAsphalt, Gravel, Snow, Ice};
    private RoadType roadTypeL;
    private RoadType roadTypeR;
    private float lengthOfRoad = 5f;
    private float lastInclination = 0f;
    [SerializeField] private float inclination = 5f;

    [SerializeField] private RoadType roadType = RoadType.Gravel;

    private Transform roadChild;
    private Transform alteredPivotChild;
    private Transform cubeChild;

    void Start()
    {
        if(roads != null && roads.Count > 0){
           roads = roads.OrderBy(r => r.transform.position.x).ToList();
        }

    }

    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roadChild = transform.Find(roads[0].name);
        alteredPivotChild = roadChild.Find("Road With Altered Pivot");
        cubeChild = alteredPivotChild.Find("Cube");
        ChangeTexture changeTextureScript = cubeChild.GetComponent<ChangeTexture>();

        switch (roadType){
            case RoadType.Gravel:
                changeTextureScript.ChangeRoadTexture("Gravel");
                break;

            case RoadType.Snow:
                changeTextureScript.ChangeRoadTexture("Snow");
                break;

            case RoadType.Ice:
                changeTextureScript.ChangeRoadTexture("Ice");
                break;

            case RoadType.WetAsphalt:
                changeTextureScript.ChangeRoadTexture("WetAsphalt");
                break;

            case RoadType.DryAsphalt:
                changeTextureScript.ChangeRoadTexture("DryAsphalt");
                break;

            default:
                break;

        }
        

        roads.Remove(movedRoad);

        if (lastInclination != inclination && lastInclination == 0f){
            float newX = roads[roads.Count - 1].transform.position.x + (lengthOfRoad * Mathf.Cos(Mathf.Deg2Rad * inclination));
            float newY = roads[roads.Count - 1].transform.position.y + (lengthOfRoad * Mathf.Sin(Mathf.Deg2Rad * inclination))/2;
            movedRoad.transform.position = new Vector3(newX, newY, 0);
            movedRoad.transform.rotation = Quaternion.Euler(0, 0, inclination);
            lastInclination = inclination;

        } else if (lastInclination == inclination){
            float newX = roads[roads.Count - 1].transform.position.x + (lengthOfRoad * Mathf.Cos(Mathf.Deg2Rad * inclination));
            float newY = roads[roads.Count - 1].transform.position.y + (lengthOfRoad * Mathf.Sin(Mathf.Deg2Rad * inclination));
            movedRoad.transform.position = new Vector3(newX, newY, 0);
            movedRoad.transform.rotation = Quaternion.Euler(0, 0, inclination);

        } else if (lastInclination != inclination && lastInclination > 0f ){
            float newX = roads[roads.Count - 1].transform.position.x + (lengthOfRoad * Mathf.Cos(Mathf.Deg2Rad * inclination));
            float newY = roads[roads.Count - 1].transform.position.y + (lengthOfRoad * Mathf.Sin(Mathf.Deg2Rad * (inclination+lastInclination)))/2;
            movedRoad.transform.position = new Vector3(newX, newY, 0);
            movedRoad.transform.rotation = Quaternion.Euler(0, 0, inclination);
            lastInclination = inclination;            
        }

        roads.Add(movedRoad);
    }
}
