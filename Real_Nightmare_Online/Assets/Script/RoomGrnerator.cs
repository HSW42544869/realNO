using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGrnerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };        //媒舉類型
    public Direction direction;

    [Header("房間信息")]
    public GameObject roomPrefab;
    public int roomNumber;                  //房間數量
    public Color starColor, endColor;       //顏色
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float Xoffset;
    public float Yoffset;
    public LayerMask roomLayer;
    public int maxStep;

    [Header("傳送門")]
    public GameObject door;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();

    List<GameObject> lessFarRooms = new List<GameObject>();

    List<GameObject> oneWayRooms = new List<GameObject>();

    public WallType walltype;
    private void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改變point位置
            ChangePoint();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = starColor;

        endRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            /*if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room.gameObject;
            }*/
            SetupRoom(room, room.transform.position);
        }
        FinEndRoom();
            endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    private void Update()
    {

    }
    public void ChangePoint()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, Yoffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -Yoffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-Xoffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(Xoffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer)); 
    }
    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, Yoffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -Yoffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-Xoffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(Xoffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(Xoffset,Yoffset);

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(walltype.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(walltype.singleBottom, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(walltype.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(walltype.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(walltype.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(walltype.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomDown)
                    Instantiate(walltype.doubleLB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(walltype.doubleUB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.doubleRB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.tripleLRB, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleURB, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(walltype.tripleLUB, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomDown && newRoom.roomLeft && newRoom.roomRight && newRoom.roomUp)
                    Instantiate(walltype.fourDoors, roomPosition, Quaternion.identity);
                break;


        }
    }
    public void FinEndRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;       
        }

        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }
        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if (oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
        door.transform.position = endRoom.transform.position;
    }


   



}

[System.Serializable]
public class WallType {
    public GameObject singleLeft, singleRight, singleUp, singleBottom,
                        doubleLU, doubleLR, doubleLB, doubleUR, doubleUB, doubleRB,
                        tripleLUR, tripleLUB, tripleURB, tripleLRB,
                        fourDoors;
    
}

