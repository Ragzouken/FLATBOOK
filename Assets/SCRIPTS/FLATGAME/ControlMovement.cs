﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlMovement : MonoBehaviour
{
    [Tooltip("Should this object move relative to the direction it is facing?")]
    public bool relativeMovement;
    [Tooltip("How many units of distance does this object move per second?")]
    public float movementSpeed = 3;

    [Header("Keyboard Input")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;

    private void Update()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(leftKey))
        {
            direction -= new Vector3(1, 0, 0);
        }

        if (Input.GetKey(rightKey))
        {
            direction += new Vector3(1, 0, 0);
        }

        if (Input.GetKey(upKey))
        {
            direction += new Vector3(0, 1, 0);
        }

        if (Input.GetKey(downKey))
        {
            direction -= new Vector3(0, 1, 0);
        }

        // Time.deltaTime tells us how many seconds have passed since we last
        // updated the movement - we multiply by movementSpeed to work out
        // how far that many seconds of movement takes us, and multiply by
        // direction to work out the final vector of movement
        Vector2 displacement = direction * movementSpeed * Time.deltaTime;

        if (relativeMovement)
        {
            transform.Translate(displacement, Space.Self);
        }
        else
        {
            transform.Translate(displacement, Space.World);
        }
    }
}
