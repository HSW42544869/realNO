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


    public List<GameObject> rooms = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));

            //改變point位置
            ChangePoint();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = starColor;

        endRoom = rooms[0];
        foreach (var room in rooms)
        {
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room;
            }
        }
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
                    generatorPoint.position += new Vector3(Xoffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(Xoffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer)); 
    } }
