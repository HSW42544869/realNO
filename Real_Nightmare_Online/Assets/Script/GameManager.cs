using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("生命")]
    public Text text;
    [Header("子彈")]
    public Text buttle;
    [Header("鑰匙")]
    public Text keytext;
    [Header("暫停")]
    public bool pausebool;
    public GameObject pauseobj;
    private Vector3 oriPosition;
    private int num0fKeys = 0;

    private GameObject player;
    
    private void Update()
    {
        Live();
        Buttle();
        pause();
        AddKey();
        
        //oriPosition = character.transform.position;
    }
    private void Awake()
    {
        player = GameObject.Find("character");
        num0fKeys = 0;
        //keytext.text = num0fKeys.ToString();
    }
    public void Live()
    {
        text.text = ""+ plsyermovement.live;       // 更新文字介面
    }
    public void Buttle()
    {
        buttle.text = "" + PlayerAimWeapon.bullet;
    }
    #region 鑰匙系統
    public int GetKeyNumbers()
    {
        return num0fKeys;
    }
    public void AddKey()
    {
        num0fKeys++;
        keytext.text = "" + plsyermovement.key;       
    }
    public void UseKey()
    {
        num0fKeys--;
        keytext.text = num0fKeys.ToString();
    }
    #endregion
    /*private void returnPosition()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            character.transform.position = oriPosition;
        }
    }*/

    public void pause()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (pausebool == true)
            {
                player.SetActive(true);
                pauseobj.SetActive(false);
                Time.timeScale = 1;
                pausebool = false;
                
            }
            else
            {
                player.SetActive(false);
                pauseobj.SetActive(true);
                Time.timeScale = 0;
                pausebool = true;
                
            }
        }
    }
}
