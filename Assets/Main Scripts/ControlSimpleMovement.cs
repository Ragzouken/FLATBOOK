using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlSimpleMovement : MonoBehaviour
{
    public float movementPerSecond = 3;

    [Header("Keyboard Input")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode downKey = KeyCode.S;

    public KeyCode upKeyAlt = KeyCode.None;
    public KeyCode leftKeyAlt = KeyCode.None;
    public KeyCode rightKeyAlt = KeyCode.None;
    public KeyCode downKeyAlt = KeyCode.None;

    private void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(leftKey) || Input.GetKey(leftKeyAlt))
        {
            direction -= new Vector3(1, 0, 0);
        }

        if (Input.GetKey(rightKey) || Input.GetKey(rightKeyAlt))
        {
            direction += new Vector3(1, 0, 0);
        }

        if (Input.GetKey(upKey) || Input.GetKey(upKeyAlt))
        {
            direction += new Vector3(0, 1, 0);
        }

        if (Input.GetKey(downKey) || Input.GetKey(downKeyAlt))
        {
            direction -= new Vector3(0, 1, 0);
        }

        transform.Translate(direction * movementPerSecond * Time.deltaTime);
    }
}
