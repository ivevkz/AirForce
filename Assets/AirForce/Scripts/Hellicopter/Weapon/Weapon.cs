using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Weapon : InputControl
{
    [SerializeField] private GameObject _point;

    public GameObject Point => _point;

    public abstract void Shoot();    
}
