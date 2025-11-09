using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    private Rigidbody rb;
    private Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = transform.localEulerAngles;
        rb.velocity = transform.forward * movementSpeed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
