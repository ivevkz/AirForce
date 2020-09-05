using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private int _amountBullet;
    [SerializeField] private float _deleyShoot;
    [SerializeField] private ParticleSystem Effect;


    public float DeleyShoot => _deleyShoot;

    public override void Shoot()
    {       
        Instantiate(_bullet, Point.transform.position, Point.transform.rotation);
        //var instanceEffect = Instantiate(Effect, Point.transform.position, Point.transform.rotation) as GameObject;
        //Effect.SetActive(true);
        Effect.Play(true);
    }
}
