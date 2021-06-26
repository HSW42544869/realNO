
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("文字")]
    public Text text;
    private void Update()
    {
        Live();
    }
    public void Live()
    {
        text.text = ""+ plsyermovement.live;       // 更新文字介面
    }
}
