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
    public GameObject endRoom;


    [Header("位置控制")]
    public Transform generatorPoint;
    public float xoffset;
    public float yoffset;
    public LayerMask roomLayer;
    public int maxStep;

    public List<Room> rooms = new List<Room>();     //創建房間列表

    List<GameObject> farRooms = new List<GameObject>();     //最遠距離的房間


    List<GameObject> lessFarRooms = new List<GameObject>();     //第二遠的房間


    List<GameObject> oneWayRooms = new List<GameObject>();      //以上兩個有哪一個是只有單獨路口的房間

    public WallType wallType;
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改變point位置
            ChangPointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = starColor;      //第一個房間

        endRoom = rooms[0].gameObject;

        //找到最後房間
        foreach (var room in rooms)
        {
            //if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)     //距離初始房間最遠的房間作為最後的房間
            //{
            //    endRoom = room.gameObject;
            //}
            SetupRoom(room, room.transform.position);
        }
        FindEndRoom();

        endRoom.GetComponent<SpriteRenderer>().color = endColor;


    }


    void Update()
    {
        // if (Input.anyKeyDown)
        // {
        //   SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //或的當前激活的場景,並從新進行加載
        // }
    }

    public void ChangPointPos()     //判斷且加房間
    {
        do   //dowhile先執行一次再做後續的判斷與後面的動作
        {
            direction = (Direction)Random.Range(0, 4);       //獲得隨機的值(0~4不包含4),加Direction轉換成媒舉型


            switch (direction)//上下左右位置隨機生成
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yoffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yoffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xoffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xoffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }
    /// <summary>
    /// 判斷上下左右
    /// </summary>
    /// <param name="newRoom"></param>
    /// <param name="roomPosition"></param>
    public void SetupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yoffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yoffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xoffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xoffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xoffset, yoffset);

        switch (newRoom.doorNumber)
        {
            case 1:     //一個路徑的房間
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUP, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleDown, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:     //兩個路徑的房間
                if (newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.doubleRD, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.doubleDL, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomRight)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUD, roomPosition, Quaternion.identity);
                break;
            case 3:     //三個路徑的房間
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.tripleURD, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown && newRoom.roomLeft)
                    Instantiate(wallType.tripleRDL, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomLeft && newRoom.roomUp)
                    Instantiate(wallType.tripleDLU, roomPosition, Quaternion.identity);
                break;
            case 4:   //全開的房間
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(wallType.fourDoors, roomPosition, Quaternion.identity);
                break;


        }
    }

    public void FindEndRoom()       //找到最後的房間
    {

        for (int i = 0; i < rooms.Count; i++)       //先判斷所有的遠處房間號
        {

            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;//將最遠的數字房間抓出來
        }
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);      //獲得數值最大房間
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);       //獲得數值次大房間
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)           //最遠距離判斷哪一個開關門數量等於1
                oneWayRooms.Add(farRooms[i]);
        }
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)       //次遠距離判斷哪一個開關門數量等於1
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if (oneWayRooms.Count != 0)     //有單個開向的房間
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            //無單個開向的房間
        }
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];        //隨機最遠距離抓一個
        }
    }
}
[System.Serializable]       //使Unity識別
public class WallType
{
    public GameObject singleLeft, singleRight, singleUP, singleDown,
                      doubleLU, doubleUR, doubleRD, doubleDL, doubleLR, doubleUD,
                      tripleLUR, tripleURD, tripleRDL, tripleDLU,
                      fourDoors;
}

