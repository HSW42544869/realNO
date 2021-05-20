using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class FieldOfview : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;   //用來偵測敵人,如果是敵人射線無遮擋遮擋
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 40f; //攻擊預設角度
        Vector3 origin = Vector3.zero;
    }
    private void LateUpdate()
    {
        int rayCount = 50;   //弧線設50個
        float angle = startingAngle ;   //角度
        float angleIncrease = fov / rayCount;   //間隔距離公式
        float viewDistance = 80f;   //距離


        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; //向量頂點
        Vector2[] uv = new Vector2[vertices.Length];    //射線區域
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)  //設定預判區域所捕捉到的物件
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);  //射線區域,回Utils程式庫抓取計算出徑度,查看距離
            if (raycastHit2D.collider == null)  //如果區域內沒有任何物件    
            {
                //No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;   //執行射線
            }
            else
            {
                //如果有,則執行vertex
            }
            {
                //Hit object
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)  //
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = triangleIndex - 1;
                triangles[triangleIndex + 2] = triangleIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;

        }

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(50, 0);
        vertices[2] = new Vector3(0, -50);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

    }

    public void SetOrigin(Vector2 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
    public void SetFov(float fov)
    {
        this.fov = fov;
    }
    public void SetviewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

}
