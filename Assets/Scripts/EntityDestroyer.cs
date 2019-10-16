using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.parent.gameObject.tag == "MovingEntity")
            collision.transform.parent.gameObject.SetActive(false);
    }
}
