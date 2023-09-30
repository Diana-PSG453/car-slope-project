using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    //output values
    public Text valueL;
    public Text valueR;
    public Slider sliderL;
    public Slider sliderR;

    [SerializeField] private float maxSliderValue = 100f;

    // variables --> ROS
        // 0 - dry asphalt
        // 1 - wet asphalt
        // 2 - gravel
        // 3 - snow
        // 4 - ice
    public float speed = 10f;
    public float incline = 0f;
    public float mass = 1000f;
    public int leftSurface = 0;
    public int rightSurface = 0;

    // ROS --> variables (to be accessed from another script)
    [SerializeField] private float torqueL = 0f;
    [SerializeField] private float torqueR = 0f;

    // Update is called once per frame
    void Update()
    {
        SetTorqueSlider();
    }

    public void SetTorqueSlider()
    {
        // variables to be accessed from another script
        // display torque in text
        valueL.text = torqueL.ToString("0.00");
        valueR.text = torqueR.ToString("0.00");
        // set slider max value
        sliderL.maxValue = maxSliderValue;
        sliderR.maxValue = maxSliderValue;
        // display torque in slider
        sliderL.value = torqueL;
        sliderR.value = torqueR;
    }

    public void ReadSpeed(string s)
    {
        speed = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log(speed);
    }

    public void ReadIncline(string s)
    {
        incline = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log(incline);
    }

    public void ReadMass(string s)
    {
        mass = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log(mass);
    }
    
    public void ReadSurfaceL(int i)
    {
        leftSurface = i;
        Debug.Log(leftSurface);
    }

    public void ReadSurfaceR(int i)
    {
        rightSurface = i;
        Debug.Log(rightSurface);
    }
}
