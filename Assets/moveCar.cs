using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }
}
