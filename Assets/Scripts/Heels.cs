using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heels : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.OnTakeHeel();
        Destroy(gameObject);
    }
    

}
