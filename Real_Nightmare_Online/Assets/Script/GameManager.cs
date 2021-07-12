
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("生命")]
    public Text text;
    [Header("子彈")]
    public Text buttle;
    [Header("鑰匙")]
    public Text keytext;

    private void Update()
    {
        Live();
        Buttle();
        Key();  
    }
    public void Live()
    {
        text.text = ""+ plsyermovement.live;       // 更新文字介面
    }
    public void Buttle()
    {
        buttle.text = "" + PlayerAimWeapon.bullet;
    }
    public void Key()
    {
        keytext.text = "" + plsyermovement.key;       
    }
}
