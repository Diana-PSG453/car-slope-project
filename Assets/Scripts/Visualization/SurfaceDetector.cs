using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public float detectionDistance = 5f;
    public GameObject LBwheel;
    public GameObject RBwheel;

    // Values will be accesed by the ROS script
    [System.NonSerialized]
    public int detectedRoadTypeL;
    [System.NonSerialized]
    public int detectedRoadTypeR;


    void HitDetector(Vector3 origin, Vector3 direction, float distance, bool right)
    {
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            GameObject detectedRoad = hit.collider.gameObject;

            if (!right)
            {
                switch (detectedRoad.tag){
                    case "DryAsphalt":
                        detectedRoadTypeL = 0;
                        break;
                    case "WetAsphalt":
                        detectedRoadTypeL = 1;
                        break;
                    case "Gravel":
                        detectedRoadTypeL = 2;
                        break;
                    case "Snow":
                        detectedRoadTypeL = 3;
                        break;
                    case "Ice":
                        detectedRoadTypeL = 4;
                        break;
                    default:
                        break;
                }
            }

            if (right)
            {
                switch (detectedRoad.tag){
                    case "DryAsphalt":
                        detectedRoadTypeR = 0;
                        break;
                    case "WetAsphalt":
                        detectedRoadTypeR = 1;
                        break;
                    case "Gravel":
                        detectedRoadTypeR = 2;
                        break;
                    case "Snow":
                        detectedRoadTypeR = 3;
                        break;
                    case "Ice":
                        detectedRoadTypeR = 4;
                        break;
                    default:
                        break;
                }
            }
        }
    }


    void FixedUpdate()
    {
        
        HitDetector(LBwheel.transform.position, Vector3.down, detectionDistance, false);
        HitDetector(RBwheel.transform.position, Vector3.down, detectionDistance, true);
        
        //Debug.Log("left: " + detectedRoadTypeL + " | right: " + detectedRoadTypeR);

    }
}

