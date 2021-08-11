using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class newfieldofview : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;   //渲染
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 40f;    //角度
        viewDistance = 80f;
        origin = Vector3.zero;
    }
    private void LateUpdate()
    {
        int rayCount = 50;  //執行的光線數量
        float angle = startingAngle;    //視野距離
        float angleIncrease = fov / rayCount;   //視野角度

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; //光線位置
        Vector2[] uv = new Vector2[vertices.Length];    //頂點位置
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;   //原點

        int vertexIndex = 1;
        int triangleIndex = 0; //三角形的索引質 = 0
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 vertex;
            //光線的投射 = 光線(原點 * 角度 * 距離)
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance,layerMask);
            if (raycastHit2D.collider == null)  //判斷如果光線2D物件為0
            {
                // 三向量 = 原點 + 角度參數 * 距離
                 vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //hit object 
                //物件那端就會是我們三角形的頂點
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex; //將頂點換為此頂點

            if(i > 0){
                    //三角形三的頂點
                    triangles[triangleIndex + 0] = 0;               //頂點0
                    triangles[triangleIndex + 1] = vertexIndex - 1; //頂點1
                    triangles[triangleIndex + 2] = vertexIndex;     //頂點2

                    triangleIndex += 3;
                }
            vertexIndex++;

            angle -= angleIncrease; //旋轉
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 animDirection)
    { 
        startingAngle = UtilsClass.GetAngleFromVectorFloat(animDirection) - fov/ 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }
    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
