using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class AnimateFaceDirection : MonoBehaviour 
{
    public float rotateTime;

    private bool started;
    private Vector2 prevPosition;
    private float angleVelocity;

    private void LateUpdate()
    {
        Vector2 nextPosition = transform.position;

        if (started)
        {
            Vector2 delta = nextPosition - prevPosition;

            float angle = Mathf.Atan2(delta.y, delta.x);

            Vector3 angles = transform.eulerAngles;
            angles.z = Mathf.SmoothDampAngle(angles.z, 
                                             angle * Mathf.Rad2Deg,
                                             ref angleVelocity,
                                             rotateTime);
            transform.eulerAngles = angles;
        }

        prevPosition = nextPosition;
        started = true;
    }
}
