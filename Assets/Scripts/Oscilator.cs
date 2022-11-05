using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVectors;
    [SerializeField] [Range(0,1)] float movementFactor;


    void Start()
    {
     startingPosition = transform.position;  
    }

    void Update()
    {
        
    }
}
