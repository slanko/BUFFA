using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPrintBufferToUI : MonoBehaviour
{
    [SerializeField] Text bufferText;
    [SerializeField] InputBuffer bufferToLookAt;
    void FixedUpdate()
    {
        string myText = "";
        InputBuffer.inputType[] input = bufferToLookAt.bufferOutput();
        for(int i = 5; i >= 0; i--)
        {
            //this is wack but it's arrows as letters. fuck you
            if (input[i] == InputBuffer.inputType.UP) myText += "C";
            if (input[i] == InputBuffer.inputType.UPRIGHT) myText += "G";
            if (input[i] == InputBuffer.inputType.RIGHT) myText += "A";
            if (input[i] == InputBuffer.inputType.DOWNRIGHT) myText += "H";
            if (input[i] == InputBuffer.inputType.DOWN) myText += "D";
            if (input[i] == InputBuffer.inputType.DOWNLEFT) myText += "F";
            if (input[i] == InputBuffer.inputType.LEFT) myText += "B";
            if (input[i] == InputBuffer.inputType.UPLEFT) myText += "E";
            if (input[i] == InputBuffer.inputType.XBUTTON) myText += "1";
            if (input[i] == InputBuffer.inputType.YBUTTON) myText += "2";
            if (input[i] == InputBuffer.inputType.ABUTTON) myText += "3";
            if (input[i] == InputBuffer.inputType.BBUTTON) myText += "4";
            if (input[i] == InputBuffer.inputType.XY) myText += "12";
            if (input[i] == InputBuffer.inputType.AB) myText += "34";
            if (input[i] == InputBuffer.inputType.XA) myText += "13";
            if (input[i] == InputBuffer.inputType.YB) myText += "24";
            if (input[i] == InputBuffer.inputType.XB) myText += "14";
            if (input[i] == InputBuffer.inputType.YA) myText += "23";
            if (input[i] == InputBuffer.inputType.XYB) myText += "123";
            if (input[i] == InputBuffer.inputType.YBA) myText += "243";
            if (input[i] == InputBuffer.inputType.BAX) myText += "431";
            if (input[i] == InputBuffer.inputType.AXY) myText += "312";
            if (input[i] == InputBuffer.inputType.XYAB) myText += "1234";
            
        }
        bufferText.text = myText;
    }
}
