using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Screenshot : MonoBehaviour
{

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Screen Shot" + DateTime.Now.Minute + DateTime.Now.Second + ".png");
            Debug.Log("save screen shot");
        }
    }
# endif
}
