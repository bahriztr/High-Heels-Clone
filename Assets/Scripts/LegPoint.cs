using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.isLegOpen = true;
        Player.Instance.isLegOpenFinish = false;
    }
    
}
