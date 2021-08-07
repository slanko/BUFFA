using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public List<Transform> playerList;
    [SerializeField] float lerpSpeed;
    [SerializeField] Transform myCam;
    Vector3 camPos;

    private void Start()
    {
        camPos = myCam.localPosition;
    }

    Vector3 calculateMidpoint()
    {
        float totalX = 0f, totalY = 0f, totalZ = 0f;

        foreach(var player in playerList)
        {
            totalX += player.position.x;
            totalY += player.position.y;
            totalZ += player.position.z;
        }
        Vector3 midpoint = new Vector3(totalX / playerList.Count, totalY / playerList.Count, totalZ / playerList.Count);
        return midpoint;
    }

    float calculateFurthestFromMidPoint()
    {
        float distToReturn = 0;
        foreach(Transform player in playerList)
        {
            if(Vector3.Distance(transform.position, player.position) > distToReturn) distToReturn = Vector3.Distance(transform.position, player.position);
        }
        return distToReturn;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerList.Count > 0) transform.position = Vector3.Lerp(transform.position, calculateMidpoint(), lerpSpeed * Time.deltaTime);
        myCam.localPosition = Vector3.Lerp(myCam.localPosition, new Vector3(camPos.x, camPos.y, camPos.z + (calculateFurthestFromMidPoint() * -1 * 0.5f)), lerpSpeed * Time.deltaTime);
    }
}
