using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public enum EnemyState { MOVING, WAITING}

    [SerializeField] EnemyState enemyState;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationValue = 180f;

    Rigidbody rb;
    bool didHit;
    [SerializeField] float timeWaiting = 1;
    float timeInCurrentState = 0;

    bool turnedAround = false;

    [SerializeField] Transform hitCheck;
    [SerializeField] float hitCheckRadius = 0.2f;
    [SerializeField] LayerMask scenarioLayer;

    Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyState = EnemyState.MOVING;
        movementDirection = new Vector3(hitCheck.transform.position.x - transform.position.x, 0f, hitCheck.transform.position.z - transform.position.z);
        rotationValue = Random.Range(90f, 200f);
    }

    private void Update()
    {
        timeInCurrentState += Time.deltaTime;
        didHit = Physics.OverlapSphere(hitCheck.position, hitCheckRadius, scenarioLayer).Length > 0;
        UpdateCurrentState();
    }

    public void UpdateCurrentState()
    {
        switch (enemyState) 
        {
            case EnemyState.MOVING:
                if (didHit)
                {
                    movementDirection = new Vector3(transform.position.x - hitCheck.transform.position.x, 0f, transform.position.z - hitCheck.transform.position.z);
                    timeInCurrentState = 0;
                    enemyState = EnemyState.WAITING;
                }
                else
                {
                    rb.velocity = movementDirection.normalized * movementSpeed;
                }
                break;
            case EnemyState.WAITING:

                Quaternion currentRotation = Quaternion.Euler(0, gameObject.transform.rotation.y, 0);
                if (timeInCurrentState < timeWaiting)
                {
                    float progress = timeInCurrentState / timeWaiting;
                    rb.velocity = Vector3.zero;
                    if (!turnedAround)
                    {
                        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, rotationValue, 0), progress);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, -rotationValue, 0), Quaternion.Euler(0, 0, 0), progress);
                    }
                }
                else
                {
                    movementDirection = new Vector3(hitCheck.transform.position.x - transform.position.x, 0f, hitCheck.transform.position.z - transform.position.z);
                    turnedAround = !turnedAround;
                    rotationValue = Random.Range(90f, 200f);
                    enemyState = EnemyState.MOVING;
                }
                break;
        }
    }
}
