using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class RocketLauncher : Weapon
{
    [SerializeField] private Missile _missile;

    private List<Transform> _pointsTransforms;
    private List<Missile> _shells = new List<Missile>();

    public event UnityAction<bool> Shoting;

    private void Start()
    {
        _pointsTransforms = Point.GetComponentsInChildren<Transform>().ToList<Transform>();

        //InstallMissles();
    }


    private void InstallMissles()
    {
        for (int i = 1; i < _pointsTransforms.Count; i++)
        {
            _shells.Add(Instantiate(_missile, _pointsTransforms[i].position, Quaternion.identity, _pointsTransforms[i]));
        }
    }

    public override void Shoot()
    {
        Instantiate(_missile, _pointsTransforms[0].position, Quaternion.identity);
        /*if (_shells[0])
        {
            _shells[0].transform.parent = null;
            _shells[0].Shooting = true;
            _shells.Remove(_shells[0]);
        }*/

    }
}
