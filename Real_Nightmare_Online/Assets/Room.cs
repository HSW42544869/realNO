using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorRight, doorLeft, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;

    public int stepToStart;     //步數變量

    public Text text;       //房間計算數值

    public int doorNumber;

    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }
    public void UpdateRoom(float xoffset, float yoffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xoffset) + (Mathf.Abs(transform.position.y / yoffset)));

        text.text = stepToStart.ToString();
        //上下左右有房間都將它壘加一
        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }*/

}
