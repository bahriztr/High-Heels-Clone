using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaker : MonoBehaviour
{
    public static GameMaker Instance;

    private void Awake()
    {
        Instance = this;
    }
   
}
