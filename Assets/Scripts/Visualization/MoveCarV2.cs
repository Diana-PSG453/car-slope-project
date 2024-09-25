using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCarV2 : MonoBehaviour
{
    
    
    [SerializeField] WheelCollider LFcollider;
    [SerializeField] WheelCollider RFcollider;
    [SerializeField] WheelCollider LBcollider;
    [SerializeField] WheelCollider RBcollider;

    [SerializeField] Transform LFwheel;
    [SerializeField] Transform RFwheel;
    [SerializeField] Transform LBwheel;
    [SerializeField] Transform RBwheel;

    [SerializeField] Rigidbody rb;

   
    public float acceleration = 100f;  //Braking and accelerating forces
    public float setVelKmh = 10f;       //Default velocity of the car. Actual value is read from GUI script


    public GameObject interfaceObject;
    private GuiManager gui;

        //Reads speed from GUI
    private void GetSpeedFromGUI()  
    {
        setVelKmh = gui.speed;
    }

        //sets target velocity for the car and follows it
    private void ControlCarSpeed()
    {
        float prevAcc = LBcollider.motorTorque;
        GetSpeedFromGUI();

        //Converts km/h to m/s
        float velMs = setVelKmh / 3.6f;
        
        if (rb.velocity.x <= velMs) {
            //Debug.Log("Меньше");
            LBcollider.motorTorque += acceleration;
            RBcollider.motorTorque += acceleration;
            LBcollider.brakeTorque = 0f;
            RBcollider.brakeTorque = 0f;
        }
        else{
            //Debug.Log("Больше");
            LBcollider.motorTorque = 0f;
            RBcollider.motorTorque = 0f;
            LBcollider.brakeTorque += acceleration;
            RBcollider.brakeTorque += acceleration;
        }
    }


        //visualises turning of the wheel meshes
    private void VisualiseWheelTurn(WheelCollider col, Transform trans)
    {
        //Get wheel collider state
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        //Set wheel tranform state
        trans.position = position;
        trans.rotation = rotation;
    }


    void Start()
    {
       gui = interfaceObject.GetComponent<GuiManager>();
    }


    void FixedUpdate()
    {
        ControlCarSpeed();
    }


    void Update()
    {
        VisualiseWheelTurn(LFcollider, LFwheel);
        VisualiseWheelTurn(RFcollider, RFwheel);
        VisualiseWheelTurn(LBcollider, LBwheel);
        VisualiseWheelTurn(RBcollider, RBwheel);
    }
}
