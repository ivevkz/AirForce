using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;

    public bool Shooting = false;

    public int Speed => _speed;
    public int Damage => _damage;

    private void Update()
    {
        //if(Shooting)
        transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.World);

    }

    

}