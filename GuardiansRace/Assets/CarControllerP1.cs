using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerP1 : CarController
{
    void Update()
    {

        if (Input.GetKey("w"))
            accelerationFactorInput = 1;
        else if (Input.GetKey("s"))
            accelerationFactorInput = -1;
        else
            accelerationFactorInput = 0;

        if (Input.GetKey("a"))
            steeringFactorInput = -1;
        else if (Input.GetKey("d"))
            steeringFactorInput = 1;
        else
            steeringFactorInput = 0;
    }
}
