using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class UniversalPlayerScript : MonoBehaviour
{
    [SerializeField] float movementSpeed, inputDeadZone;
    InputBuffer buffer;
    int bufferLength;
    public bool xDown, yDown, aDown, bDown;
    //InputBuffer.inputType lastDir; //took this out because of the function holding up the input buffer - maybe reenable it for certain character?
    [SerializeField] Animator anim;
    [SerializeField] Animation myAnimation;
    [SerializeField] AnimatableValues animValues;
    MoveScriptableObject currentMove;
    int currentMoveFrame;

    [SerializeField, Header("Jumping")] AnimationCurve jumpArc;
    [SerializeField] float jumpSpeed, jumpDist;
    bool airborne = false;

    [SerializeField, Header("Move List"), Tooltip("PUT LONGER INPUTS FIRST!! OTHERWISE THEY DON'T GET FIRED")] MoveScriptableObject[] specialMoves, normals, movementInputs;

    [Header("Targeting")] public bool leftOfTarget;
    [SerializeField] Transform target, myVisual;

    #region inputHandling
    Vector2 moveVals = Vector2.zero;
    public void XBUTTON(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            xDown = true;
            handleButtonInputs();
        }
        if (context.canceled) xDown = false;
    }
    //hey you can add negative edge with context.cancelled - remember this
    public void YBUTTON(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            yDown = true;
            handleButtonInputs();
        }
        if (context.canceled) yDown = false;
    }    
    public void ABUTTON(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aDown = true;
            handleButtonInputs();
        }
        if (context.canceled) aDown = false;
    }    
    public void BBUTTON(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            bDown = true;
            handleButtonInputs();
        }
        if (context.canceled) bDown = false;
    }

    public void XY(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            xDown = true;
            yDown = true;
            handleButtonInputs();
        }
        if (context.canceled)
        {
            xDown = false;
            yDown = false;
        }
    }

    public void AB(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aDown = true;
            bDown = true;
            handleButtonInputs();
        }
        if (context.canceled)
        {
            aDown = false;
            bDown = false;
        }
    }

    public void handleButtonInputs()
    {
        var currentBufferOutput = buffer.bufferOutput();
        //X, Y, A, B, XY, AB, XA, YB, XB, YA, XYB, YBA, BAX, AXY, XYBA, FUCK YOU!!
        if (xDown && !yDown && !aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.XBUTTON);
        if (!xDown && yDown && !aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.YBUTTON);
        if (!xDown && !yDown && aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.ABUTTON);
        if (!xDown && !yDown && !aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.BBUTTON);
        //DOUBLES
        if (xDown && yDown && !aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.XY);
        if (!xDown && !yDown && aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.AB);
        if (xDown && !yDown && aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.XA);
        if (!xDown && yDown && !aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.YB);
        if (xDown && !yDown && !aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.XB);
        if (!xDown && yDown && aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.YA);
        //TRIPLES
        if (xDown && yDown && !aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.XYB);
        if (!xDown && yDown && aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.YBA);
        if (xDown && !yDown && aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.BAX);
        if (xDown && yDown && aDown && !bDown) sendInputToInputBuffer(InputBuffer.inputType.AXY);
        //THE FINALE
        if (xDown && yDown && aDown && bDown) sendInputToInputBuffer(InputBuffer.inputType.XYAB);
        //this has dealt me irreperable psych damage
    }

    public void getMovementInputs(InputAction.CallbackContext context)
    {
        moveVals = context.ReadValue<Vector2>();
    }

    public void sendInputToInputBuffer(InputBuffer.inputType dir)
    {
        buffer.addToBuffer(dir);
    }

    public InputBuffer.inputType getDirFromVector2(Vector2 dirVector)
    {
        InputBuffer.inputType input = new InputBuffer.inputType();
        //this part is gonna SUCK
        if (dirVector.magnitude > inputDeadZone || dirVector.magnitude < -inputDeadZone)
        {
            if (dirVector.x >= 0)
            {
                float tempDir = Vector2.Angle(Vector2.up, dirVector);
                if (tempDir > 0 && tempDir <= 22.5f) input = InputBuffer.inputType.UP;
                if (tempDir > 22.5f && tempDir <= 67.5f) input = InputBuffer.inputType.UPRIGHT;
                if (tempDir > 67.5f && tempDir <= 117.5f) input = InputBuffer.inputType.RIGHT;
                if (tempDir > 117.5f && tempDir <= 157.5f) input = InputBuffer.inputType.DOWNRIGHT;
                if (tempDir > 157.5f && tempDir <= 180f) input = InputBuffer.inputType.DOWN;
            }
            if (dirVector.x <= 0)
            {
                float tempDir = Vector2.Angle(Vector2.down, dirVector);
                if (tempDir > 0 && tempDir <= 22.5f) input = InputBuffer.inputType.DOWN;
                if (tempDir > 22.5f && tempDir <= 67.5f) input = InputBuffer.inputType.DOWNLEFT;
                if (tempDir > 67.5f && tempDir <= 117.5f) input = InputBuffer.inputType.LEFT;
                if (tempDir > 117.5f && tempDir <= 157.5f) input = InputBuffer.inputType.UPLEFT;
                if (tempDir > 157.5f && tempDir <= 180f) input = InputBuffer.inputType.UP;
            }
        }
        else input = InputBuffer.inputType.NEUTRAL;
        return input;
    }

    void handleInputs()
    {
        InputBuffer.inputType dir = getDirFromVector2(moveVals);
        //if (dir != lastDir || dir == InputBuffer.inputType.NEUTRAL) // this is questionable - shouldn't we allow all inputs and scrub the list until we get the one we need??
        buffer.addToBuffer(dir); // yes we should. otherwise the input buffer would just STOP AND WAIT until you'd done the whole input if you kept buttons held.
        //lastDir = dir;
    }

    #endregion

    #region bufferOutputHandling

    InputBuffer.inputType[] currentBufferOutput;
    InputBuffer.inputType currentDir;

    void bufferReadUpdate()
    {
        currentBufferOutput = buffer.bufferOutput();
        currentDir = currentBufferOutput[0];
        if (!myAnimation.isPlaying || (myAnimation.isPlaying && currentMoveFrame >= currentMove.specialCancelTime && currentMove.specialCancelTime != 0))
        {
            bool foundSpecial = false, foundNormal = false;
            foreach (MoveScriptableObject move in specialMoves)
            {
                if(checkMove(move, currentBufferOutput))
                {
                    foundSpecial = true;
                    Debug.Log("found special!");
                    if(myAnimation.isPlaying) cancelCurrentMove();
                    startMove(move);
                    break;
                }
            }
            if (!foundSpecial)
            {
                foreach(MoveScriptableObject move in normals)
                {
                    if(checkMove(move, currentBufferOutput, 5))
                    {
                        Debug.Log("found normal!");
                        foundNormal = true;
                        if (myAnimation.isPlaying) cancelCurrentMove();
                        startMove(move);
                        break;
                    }
                }
            }
            if (!foundNormal)
            {
                foreach(MoveScriptableObject move in movementInputs)
                {
                    if (checkMove(move, currentBufferOutput, 5))
                    {
                        Debug.Log("found movement!");
                        if (myAnimation.isPlaying) cancelCurrentMove();
                        startMove(move);
                        break;
                    }
                }
            }
        }
    }
    //get input buffer
    InputBuffer.inputType getFlippedInput(InputBuffer.inputType currentInput)
    {
        InputBuffer.inputType input = currentInput;

        switch (input)
        {
            case InputBuffer.inputType.LEFT:
                input = InputBuffer.inputType.RIGHT;
                break;
            case InputBuffer.inputType.RIGHT:
                input = InputBuffer.inputType.LEFT;
                break;
            case InputBuffer.inputType.DOWNLEFT:
                input = InputBuffer.inputType.DOWNRIGHT;
                break;
            case InputBuffer.inputType.DOWNRIGHT:
                input = InputBuffer.inputType.DOWNLEFT;
                break;
            case InputBuffer.inputType.UPLEFT:
                input = InputBuffer.inputType.UPRIGHT;
                break;
            case InputBuffer.inputType.UPRIGHT:
                input = InputBuffer.inputType.UPLEFT;
                break;
        }
        return input;
    }

    bool checkMove(MoveScriptableObject move , InputBuffer.inputType[] currentBuffer, int bufLength = 0)
    {
        if (bufLength == 0) bufLength = currentBuffer.Length - 1;
        bool moveGood = true;
        if (move != currentMove)
        {
            int startPoint = bufLength;
            foreach (InputBuffer.inputType motion in move.inputRequired)
            {
                for (int i = startPoint; i > 0; i--)
                {
                    if (motion == currentBuffer[i] && leftOfTarget)
                    {
                        moveGood = true;
                        startPoint = i;
                        break;
                    }
                    else if (motion == getFlippedInput(currentBuffer[i]) && !leftOfTarget)
                    {
                        moveGood = true;
                        startPoint = i;
                        break;
                    }
                    else moveGood = false;
                }
                if (moveGood == false) break;
            }
        }
        else moveGood = false;
        return moveGood;
    }

    void cancelCurrentMove()
    {
            myAnimation.Stop();
            StopCoroutine(currentDisablerCoroutine);
            currentMove.moveAnim.legacy = false;
            currentMove = new MoveScriptableObject();
    }

    void startMove(MoveScriptableObject move)
    {
        buffer.clearBuffer();
        animValues.XMoveMultiplier = 0;
        //LEGACY COMPONENTS?? VOMIT EMOJI
        currentMove = move;
        currentMove.moveAnim.legacy = true;
        myAnimation.AddClip(move.moveAnim, "MOVE");
        myAnimation.Play("MOVE");
        currentDisablerCoroutine = StartCoroutine(disableAnimatorForAMove());
        Debug.Log("Move good!");
    }

    Coroutine currentDisablerCoroutine;
    IEnumerator disableAnimatorForAMove()
    {
        currentMoveFrame = 0;
        anim.enabled = false;
        while (myAnimation.isPlaying)
        {
            currentMoveFrame++;
            yield return new WaitForFixedUpdate();
        }
        currentMove.moveAnim.legacy = false; // this is FUCKING ANNOYING!! they have to be legacy to play but have to not be legacy to be edited in unity. CRINGE!!
        currentMove = new MoveScriptableObject();
        anim.enabled = true;
        anim.Play("Idle");
    }

    IEnumerator clearTriggerWithDelay(string trigger)
    {
        yield return new WaitForSeconds(0.1f);
        anim.ResetTrigger(trigger);
        if (anim.GetBool(trigger)) Debug.Log("SHIT!!");
        else Debug.Log("YUH");
    }

    #endregion

    private void Start()
    {
        buffer = GetComponent<InputBuffer>();
        bufferLength = buffer.bufferOutput().Length - 1;
        //lastDir = InputBuffer.inputType.NEUTRAL;
    }

    private void FixedUpdate()
    {
        if (!anim.GetBool("Crouching") && !myAnimation.isPlaying)
        {
            if (currentDir == InputBuffer.inputType.RIGHT) transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
            if (currentDir == InputBuffer.inputType.LEFT) transform.Translate(new Vector3(-movementSpeed * Time.deltaTime, 0, 0));
            if (currentDir == InputBuffer.inputType.UP || currentDir == InputBuffer.inputType.UPRIGHT || currentDir == InputBuffer.inputType.UPLEFT)
            {
                StartCoroutine(jump(currentDir));
                anim.SetTrigger("Jump");
                StartCoroutine(clearTriggerWithDelay("Jump"));
            }
        }
        if (myAnimation.isPlaying && leftOfTarget) transform.Translate(new Vector3(animValues.XMoveMultiplier, 0, 0) * Time.deltaTime);
        else if (myAnimation.isPlaying && !leftOfTarget) transform.Translate(new Vector3(-animValues.XMoveMultiplier, 0, 0) * Time.deltaTime);

        if (!myAnimation.isPlaying) animValues.XMoveMultiplier = 0;
        leftOfTarget = transform.position.x < target.transform.position.x;
        if (leftOfTarget) myVisual.transform.localScale = new Vector3(1,1,1);
        else myVisual.transform.localScale = new Vector3(1, 1, -1);

        handleInputs();
        animatorUpdate();
        bufferReadUpdate();
    }

    void animatorUpdate()
    {
        if (leftOfTarget)
        {
            anim.SetBool("WalkingForward", currentDir == InputBuffer.inputType.RIGHT);
            anim.SetBool("WalkingBackward", currentDir == InputBuffer.inputType.LEFT);
        }
        else
        {
            anim.SetBool("WalkingForward", currentDir == InputBuffer.inputType.LEFT);
            anim.SetBool("WalkingBackward", currentDir == InputBuffer.inputType.RIGHT);
        }
        anim.SetBool("Airborne", airborne);
        anim.SetBool("Crouching", currentDir == InputBuffer.inputType.DOWN || currentDir == InputBuffer.inputType.DOWNRIGHT || currentDir == InputBuffer.inputType.DOWNLEFT);
    }

    IEnumerator jump(InputBuffer.inputType input)
    {
        if(airborne == false && (input == InputBuffer.inputType.UP || input == InputBuffer.inputType.UPRIGHT || input == InputBuffer.inputType.UPLEFT))
        {
            Vector3 pos = transform.position;
            airborne = true;
            for (float i = 0; i < 1; i += (jumpSpeed * Time.deltaTime))
            {
                if(input == InputBuffer.inputType.UP) transform.position = new Vector3(pos.x, pos.y + jumpArc.Evaluate(i), pos.z);
                if(input == InputBuffer.inputType.UPRIGHT) transform.position = new Vector3(pos.x + (i * jumpDist), pos.y + jumpArc.Evaluate(i), pos.z);
                if(input == InputBuffer.inputType.UPLEFT) transform.position = new Vector3(pos.x + -(i * jumpDist), pos.y + jumpArc.Evaluate(i), pos.z);
                yield return new WaitForEndOfFrame();
                anim.SetFloat("JumpPercent", i);
            }
            airborne = false;
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
    }


}
