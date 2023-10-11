using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeTexture : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cube;
    [SerializeField] private Material Ice;
    [SerializeField] private Material Snow;
    [SerializeField] private Material DryAshpalt;
    [SerializeField] private Material WetAshpalt;
    [SerializeField] private Material Gravel;


    private Renderer cubeRenderer;
    void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void ChangeRoadTexture(string roadType){
        Renderer cubeRenderer = this.GetComponent<Renderer>();
        
        switch (roadType){
            case "Ice":
                cubeRenderer.material = Ice;
                break;

            case "Snow":
                cubeRenderer.material = Snow;
                break;

            case "DryAsphalt":
                cubeRenderer.material = DryAshpalt;
                break;

            case "WetAsphalt":
                cubeRenderer.material = WetAshpalt;
                break;

            case "Gravel":
                cubeRenderer.material = Gravel;
                break;

            default:
                break;
        }
    }
}
