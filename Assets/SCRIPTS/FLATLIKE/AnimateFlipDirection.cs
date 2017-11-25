using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class AnimateFlipDirection : MonoBehaviour 
{
    public bool resetWhenStationary;
    public bool flipHorizontal;
    public bool flipVertical;

    private Vector2 prevPosition;
    private bool flippedHorizontal;
    private bool flippedVertical;

    private void Awake()
    {
        prevPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector2 nextPosition = transform.position;
        Vector2 delta = nextPosition - prevPosition;

        bool resting = !resetWhenStationary && delta.magnitude <= 0.001f;

        if (flipHorizontal && !resting)
        {
            SetFlippedHorizontal(delta.x < 0);
        }

        if (flipVertical && !resting)
        {
            SetFlippedVertical(delta.y < 0);
        }

        prevPosition = nextPosition;
    }

    public void SetFlippedHorizontal(bool flipped)
    {
        if (flippedHorizontal != flipped)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            flippedHorizontal = flipped;
        }
    }

    public void SetFlippedVertical(bool flipped)
    {
        if (flippedVertical != flipped)
        {
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;

            flippedVertical = flipped;
        }
    }
}
