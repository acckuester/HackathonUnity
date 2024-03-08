using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerP2 : CarController
{

    void Update()
    {

        if (Input.GetKey("up"))
            accelerationFactorInput = 1;
        else if (Input.GetKey("down"))
            accelerationFactorInput = -1;
        else
            accelerationFactorInput = 0;

        if (Input.GetKey("left"))
            steeringFactorInput = -1;
        else if (Input.GetKey("right"))
            steeringFactorInput = 1;
        else
            steeringFactorInput = 0;
    }
}
