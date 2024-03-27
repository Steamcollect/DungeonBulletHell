using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public Transform middleLeft, middleCenter, middleRight, buttomLeft, buttomCenter, buttomRight, topLeft, topCenter, topRight;

    public void ChangeGroundPos(Vector2 currengGroundPos)
    {
        middleCenter.position = currengGroundPos;
        middleLeft.position = new Vector2(currengGroundPos.x - 28, currengGroundPos.y);
        middleRight.position = new Vector2(currengGroundPos.x + 28, currengGroundPos.y);
        buttomCenter.position = new Vector2(currengGroundPos.x, currengGroundPos.y - 28);
        topCenter.position = new Vector2(currengGroundPos.x, currengGroundPos.y + 28);
        buttomLeft.position = new Vector2(currengGroundPos.x - 28, currengGroundPos.y - 28);
        topRight.position = new Vector2(currengGroundPos.x + 28, currengGroundPos.y + 28);
        buttomRight.position = new Vector2(currengGroundPos.x + 28, currengGroundPos.y - 28);
        topLeft.position = new Vector2(currengGroundPos.x - 28, currengGroundPos.y + 28);
    }
}