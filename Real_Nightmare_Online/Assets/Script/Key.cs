using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("生成鑰匙")]
    public GameObject key;

    public void Keydown()
    {
        if(Egg.elive <= 0)
        {
            Instantiate(key, transform.position, transform.rotation);
        }
    } 
}
