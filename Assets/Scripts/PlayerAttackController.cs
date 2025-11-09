using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    InputManager inputManager;
    AudioManager audioManager;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootCooldown = 1f;

    private float timeToShoot = 0;


    private void Start()
    {
        inputManager = InputManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Update()
    {
        timeToShoot -= Time.deltaTime;
        OnAttack();
    }

    public void OnAttack()
    {
        if (inputManager.shoot_ia.IsPressed() && timeToShoot <= 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            audioManager.PlaySFX(audioManager.playerShoot);
            timeToShoot = shootCooldown;
        }
    }
}
