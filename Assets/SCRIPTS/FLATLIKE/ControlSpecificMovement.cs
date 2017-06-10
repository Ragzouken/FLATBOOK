using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class ControlSpecificMovement : MonoBehaviour 
{
    public float movementPerSecond;
    public KeyCode forwardKey;
    public KeyCode reverseKey;
    public Vector3 moveDirection;

    private void Update()
    {
        if (Input.GetKey(forwardKey))
        {
            transform.Translate(moveDirection * movementPerSecond * Time.deltaTime);
        }

        if (Input.GetKey(reverseKey))
        {
            transform.Translate(-moveDirection * movementPerSecond * Time.deltaTime);
        }
    }
}
