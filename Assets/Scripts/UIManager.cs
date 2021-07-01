using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text diamondText;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateDiamondText(int diamondScore)
    {
        diamondText.text = diamondScore.ToString();
    }
}
