using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElement : MonoBehaviour
{
    public GameObject GameObject;
    public MovingEntity MovingEntityScript;

    private void OnValidate()
    {
        GameObject = gameObject;
        MovingEntityScript = GetComponent<MovingEntity>();
    }
}
