using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.OnTakeDiamond();
        UIManager.Instance.UpdateDiamondText(Player.Instance.diamondScore);
        Destroy(gameObject);
    }
    

}
