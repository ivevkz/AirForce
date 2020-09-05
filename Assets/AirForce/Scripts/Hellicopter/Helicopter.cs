using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Helicopter : InputControl
{
    [SerializeField] private float _enginePower;

    [Space(3)]
    [SerializeField] private List<RocketLauncher> _weapons;

    [SerializeField] private GameObject _points;

    private List<Transform> _pointsTransforms;
    private MachineGun _machineGun;
    private float _delayShoot = 0;

    public float EnginePower => _enginePower;

    private void OnEnable()
    {
        FireMachineGun += ShootMachineGun;
            FireRocket += ShootRocket;
    }

    private void OnDisable()
    {
        FireMachineGun -= ShootRocket;
        FireRocket -= ShootMachineGun;
    }

    private void Start()
    {
        _machineGun = GetComponent<MachineGun>();
        _pointsTransforms = _points.GetComponentsInChildren<Transform>().ToList<Transform>();
        InstallMissles();
    }

    private void Update()
    {
        base.HadleInputs();
        _delayShoot += Time.deltaTime;
    }

    private void ShootMachineGun()
    {       

        if (_delayShoot <= _machineGun.DeleyShoot)
            return;

        _machineGun.Shoot();
        _delayShoot = 0;
    }
    private void ShootRocket()
    {      

    }


    private void InstallMissles()
    {
        /*for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i])            
            Instantiate(_weapons[i], _pointsTransforms[i + 1].position, Quaternion.identity, _pointsTransforms[i + 1]);            
        }*/
    }
}
