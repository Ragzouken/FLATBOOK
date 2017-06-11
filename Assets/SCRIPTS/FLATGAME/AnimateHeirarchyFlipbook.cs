using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

/// <summary>
/// animate by treating all children of this object as a frame of an animation,
/// turning them off and on in sequence, ensuring only one is ever on at a time
/// </summary>
[ExecuteInEditMode]
public class AnimateHeirarchyFlipbook : MonoBehaviour 
{
    [SerializeField]
    private int currentFrame;

    [Header("Timings (seconds)")]
    [SerializeField]
    [Tooltip("How many seconds each frame is shown for")]
    private float frameDuration = .1f;
    [SerializeField]
    [Tooltip("How many seconds it takes the animation to complete")]
    private float loopDuration;
    [SerializeField]
    [Tooltip("Should the animation start part way through a random frame?")]
    private bool startOnRandomTime;

    private float timeSinceCurrentFrame;

    #region Editor Tricks
    private float _prevFrameDuration;
    private float _prevLoopDuration;
    private int _prevFrameCount;

    /// <summary>
    /// update timings so that loop duration, frame duration, and frame count
    /// are consistent with each other, based on which have just changed
    /// </summary>
    private void MakeTimingsConsistent()
    {
        int frameCount = transform.childCount;

        if (frameCount == 0)
        {
            return;
        }

        if (_prevFrameCount != frameCount)
        {
            loopDuration = frameCount * frameDuration;
        }
        else if (_prevFrameDuration != frameDuration)
        {
            loopDuration = frameCount * frameDuration;
        }
        else if (_prevLoopDuration != loopDuration)
        {
            frameDuration = loopDuration / frameCount;
        }

        _prevFrameCount = frameCount;
        _prevFrameDuration = frameDuration;
        _prevLoopDuration = loopDuration;
    }
    #endregion

    private void Start()
    {
        if (startOnRandomTime)
        {
            currentFrame = Random.Range(0, transform.childCount);
            timeSinceCurrentFrame = Random.value * frameDuration;
        }
    }

    private void Update()
    {
        MakeTimingsConsistent();

        AdvanceTime(Time.deltaTime);
    }

    /// <summary>
    /// advance the animation by a duration in seconds, advancing as many
    /// frames as necessary
    /// </summary>
    private void AdvanceTime(float duration)
    {
        timeSinceCurrentFrame += duration;
        
        while (timeSinceCurrentFrame > frameDuration && frameDuration > 0)
        {
            timeSinceCurrentFrame -= frameDuration;

            AdvanceFrame();
        }
    }

    /// <summary>
    /// advance the animation by one frame, looping if necessary
    /// </summary>
    private void AdvanceFrame()
    {
        currentFrame += 1;

        // looping
        if (currentFrame >= transform.childCount)
        {
            currentFrame = 0;
        }

        RefreshFrame();
    }

    /// <summary>
    /// turn off all frames except the current frame, which is turned on
    /// </summary>
    private void RefreshFrame()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(i == currentFrame);
        }
    }
}
