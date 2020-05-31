using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int fps;
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = fps;
    }
}
