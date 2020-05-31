using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int maxFps;
    private int prevMaxFps;

    void Awake()
    {
        prevMaxFps = maxFps;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = maxFps;
    }

    private void Update()
    {
        //Only change fps if fpsVar changes
        if(maxFps != prevMaxFps)
        {
            Application.targetFrameRate = maxFps;
            prevMaxFps = maxFps;
        }
    }
}
