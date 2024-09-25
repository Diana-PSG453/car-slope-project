using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    // As later in the script we are casting int into enum, it is essential to have enumerators in the same sequence as in GuiManager class
    public enum RoadType {DryAsphalt, WetAsphalt, Gravel, Snow, Ice}; 
    private float lengthOfRoad = 5f;
    private float lastInclination = 0f;
    public float inclination = 5f;

    [SerializeField] private RoadType roadTypeL = RoadType.DryAsphalt;
    [SerializeField] private RoadType roadTypeR = RoadType.DryAsphalt;
    private Transform roadChild;
    private Transform alteredPivotChild;
    private Transform cubeChild;

    void Start()
    {
        if(roads != null && roads.Count > 0){
           roads = roads.OrderBy(r => r.transform.position.x).ToList();
        }

    }

    //Setup for variable acces from GUI
    public GameObject interfaceObject;
    private GuiManager gui;

    private void GetInclineFromGUI()
    {
        gui = interfaceObject.GetComponent<GuiManager>();
        inclination = gui.incline;
    }

        //Gets road surface type value from GUI
    private void GetRoadTypeFromGUI()
    {
        gui = interfaceObject.GetComponent<GuiManager>();
        //Unity's GUI functionality doesn't allow to use enums, so int is casted into enum here
        roadTypeL = (RoadType)gui.roadTypeIntL;
        roadTypeR = (RoadType)gui.roadTypeIntR;
        //Debug.Log(roadTypeL);       
    }

        // applies texture to road and assigs corresponding tag
    private void ChangingTexture()
    {
        roadChild = transform.Find(roads[0].name);
        alteredPivotChild = roadChild.Find("Road With Altered Pivot");
        cubeChild = alteredPivotChild.Find("CubeL");
        ChangeTexture changeTextureScript = cubeChild.GetComponent<ChangeTexture>();

        switch (roadTypeL){
            case RoadType.DryAsphalt:
                changeTextureScript.ChangeRoadTexture("DryAsphalt");
                cubeChild.tag = "DryAsphalt";
                break;
            
            case RoadType.WetAsphalt:
                changeTextureScript.ChangeRoadTexture("WetAsphalt");
                cubeChild.tag = "WetAsphalt";
                break;

            case RoadType.Gravel:
                changeTextureScript.ChangeRoadTexture("Gravel");
                cubeChild.tag = "Gravel";
                break;

            case RoadType.Snow:
                changeTextureScript.ChangeRoadTexture("Snow");
                cubeChild.tag = "Snow";
                break;

            case RoadType.Ice:
                changeTextureScript.ChangeRoadTexture("Ice");
                cubeChild.tag = "Ice";
                break;

            default:
                break;

        }
        
        cubeChild = alteredPivotChild.Find("CubeR");
        changeTextureScript = cubeChild.GetComponent<ChangeTexture>();

        switch (roadTypeR){
            case RoadType.DryAsphalt:
                changeTextureScript.ChangeRoadTexture("DryAsphalt");
                cubeChild.tag = "DryAsphalt";
                break;

            case RoadType.WetAsphalt:
                changeTextureScript.ChangeRoadTexture("WetAsphalt");
                cubeChild.tag = "WetAsphalt";
                break;

            case RoadType.Gravel:
                changeTextureScript.ChangeRoadTexture("Gravel");
                cubeChild.tag = "Gravel";
                break;

            case RoadType.Snow:
                changeTextureScript.ChangeRoadTexture("Snow");
                cubeChild.tag = "Snow";
                break;

            case RoadType.Ice:
                changeTextureScript.ChangeRoadTexture("Ice");
                cubeChild.tag = "Ice";
                break;

            default:
                break;

        }
    }


        // Moves the road with all of the remaining functionality 
    public void MoveRoad()
    {
        GetInclineFromGUI();
        GetRoadTypeFromGUI();
        
        GameObject movedRoad = roads[0];
        ChangingTexture();

        roads.Remove(movedRoad);


            // Only God knows what happens here
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
        } else if (lastInclination != inclination && lastInclination < 0f ){
            float newX = roads[roads.Count - 1].transform.position.x + (lengthOfRoad * Mathf.Cos(Mathf.Deg2Rad * inclination));
            float newY = roads[roads.Count - 1].transform.position.y + (lengthOfRoad * Mathf.Sin(Mathf.Deg2Rad * (inclination+lastInclination)))/2;
            movedRoad.transform.position = new Vector3(newX, newY, 0);
            movedRoad.transform.rotation = Quaternion.Euler(0, 0, inclination);
            lastInclination = inclination;          

        }

        roads.Add(movedRoad);
    }
}
