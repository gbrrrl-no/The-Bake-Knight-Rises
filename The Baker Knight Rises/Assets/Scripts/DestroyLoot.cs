using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLoot : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject root = collision.transform.root.gameObject;
        if (root.CompareTag("Player"))
        {
            Destroy(transform.root.gameObject);
        }
    }
}
