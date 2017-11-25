using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class AnimateTurnDirection : MonoBehaviour 
{
    public bool resetWhenStationary;

    [Header("Timings (seconds??)")]
    public float rotateTime;

    private float startAngle;
    private Vector2 prevPosition;
    private float angleVelocity;
    private bool flipped;

    private void Awake()
    {
        prevPosition = transform.position;
        startAngle = transform.eulerAngles.z;
    }

    private void LateUpdate()
    {
        Vector2 nextPosition = transform.position;
        Vector2 delta = nextPosition - prevPosition;
        Vector3 angles = transform.eulerAngles;
        float angle = transform.eulerAngles.z;

        float target; 

        if (delta.magnitude > 0.001f)
        {
            target = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        }
        else
        {
            if (resetWhenStationary)
            {
                target = startAngle;
            }
            else
            {
                target = angle;
            }
        }
        
        angles.z = Mathf.SmoothDampAngle(angle,
                                         target,
                                         ref angleVelocity,
                                         rotateTime);
        transform.eulerAngles = angles;

        prevPosition = nextPosition;
    }
}
