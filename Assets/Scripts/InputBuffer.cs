using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    public enum inputType
    {
        DOWNLEFT, 
        DOWN, 
        DOWNRIGHT, 
        LEFT, 
        NEUTRAL, 
        RIGHT, 
        UPLEFT, 
        UP, 
        UPRIGHT,
        XBUTTON,
        YBUTTON,
        ABUTTON,
        BBUTTON,
        XY,
        AB,
        XA,
        YB,
        XB,
        YA,
        XYB,
        YBA,
        BAX,
        AXY,
        XYAB
    }
    [SerializeField] inputType[] buffer;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void addToBuffer(inputType input)
    {
        inputType temp1 = buffer[0];
        buffer[0] = input;
        for(int i = 1; i < buffer.Length; i++)
        {
            inputType temp2 = buffer[i];
            buffer[i] = temp1;
            temp1 = temp2;
        }
    }

    public void clearBuffer()
    {
        for(int i = 0; i < buffer.Length - 1; i++)
        {
            buffer[i] = inputType.NEUTRAL;
        }
    }

    void FixedUpdate()
    {

    }

    public inputType[] bufferOutput()
    {
        return buffer;
    }

}
