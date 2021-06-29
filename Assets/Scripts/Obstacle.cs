using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int obstacleCount;

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.DestroyCharacter(obstacleCount);
    }
}
