using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsBehaviour : MonoBehaviour
{
    private Vector3 lastPostion;
    public Vector3 currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = lastPostion - transform.position / Time.deltaTime;
        lastPostion = transform.position;
    }
}
