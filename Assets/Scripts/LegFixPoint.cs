using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegFixPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.isLegFixOpen = true;
        Player.Instance.isLegOpen = false;
    }
}
