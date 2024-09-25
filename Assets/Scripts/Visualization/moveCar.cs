using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour
{
    public Rigidbody rb;
    public double thrustZ = 10f;
    private Vector3 thrust = new Vector3(0f, 0f, 1);
    [SerializeField] public Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = velocity;
        rb.AddForce(thrust);
    }
}
