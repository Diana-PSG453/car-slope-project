using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    public enum RoadType {Normal, Ice, Wet};
    public RoadType roadType;
    private float lengthOfRoad = 5f;
    private float lastInclination = 0f;
    [SerializeField] public float inclination = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if(roads != null && roads.Count > 0){
           roads = roads.OrderBy(r => r.transform.position.x).ToList();
        }
    }

    public void MoveRoad()
    {
        //GameObject movedRoad;
        GameObject movedRoad = roads[0];
        /*switch (roadtype)
        {
            case RoadType.Normal:
                movedRoad = normalRoads[0];
                return;

            case RoadType.Ice:
                return;

            case RoadType.Wet:
                return;
            
            default:
                return;
        }*/

        roads.Remove(movedRoad);
        
        
        if (lastInclination != inclination && lastInclination == 0f){
            Debug.Log("A");
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
