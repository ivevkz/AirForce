using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;    

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
    }
}
