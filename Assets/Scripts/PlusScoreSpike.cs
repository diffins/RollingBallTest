﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScoreSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.AddScore(2);
        }
    }
}
