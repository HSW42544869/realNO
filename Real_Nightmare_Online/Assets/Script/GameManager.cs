
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("生命")]
    public Text text;
    [Header("子彈")]
    public Text buttle;

    private void Update()
    {
        Live();
        Buttle();
    }
    public void Live()
    {
        text.text = ""+ plsyermovement.live;       // 更新文字介面
    }
    public void Buttle()
    {
        buttle.text = "" + PlayerAimWeapon.bullet;
    }
}
