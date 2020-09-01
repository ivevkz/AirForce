using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Helipad : InputControl
{
    [SerializeField] private Transform _landingPoint;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private float _landingTime;
    [SerializeField] private float _takeOffTime;

    private Mover _mover;
    private float _currentTime;
    private bool _isAtion = false;

    private Vector3 _currentPosition;
    private Quaternion _currentRotation;

    public event UnityAction TheActionIsOver;

    private void OnEnable()
    {
        StartAction += PressE;
    }

    private void OnDisable()
    {
        StartAction -= PressE;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TryGetComponent(out Mover mover);
            _mover = mover;
        }
    }

    private void Update()
    {
        base.HadleInputs();
        if (_mover != null && _isAtion && !_mover.IsGround)      
            Landing();        
        else if (_mover != null && _isAtion && _mover.IsGround)        
            TakeOff();        
    }

    private void PressE(bool isAction)
    {
        _currentPosition = _mover.GetComponentInParent<Transform>().position;
        _currentRotation = _mover.GetComponentInParent<Transform>().rotation;
        _isAtion = isAction;
    }
    
    private void Landing()
    {
            _currentTime += Time.deltaTime;
            _mover.GetComponentInParent<Transform>().position = Vector3.Lerp(_currentPosition, _landingPoint.position, _moveCurve.Evaluate(_currentTime / _landingTime));  
            _mover.GetComponentInParent<Transform>().rotation = Quaternion.Lerp(_currentRotation, _landingPoint.rotation, _moveCurve.Evaluate(_currentTime / _landingTime));

        //_mover.GetComponentInParent<Transform>().GetComponentInChildren<Transform>().rotation = Quaternion.Lerp(_mover.GetComponent<Transform>().GetComponentInChildren<Transform>().rotation, new Quaternion(-1.5f, 0, 0, 0), _moveCurve.Evaluate(_currentTime / _LandingTime));
        //_mover.gameObject.GetComponentInChildren<Transform>().rotation

        //Debug.Log(_mover.GetComponentInChildren<Transform>().name);
    }
    private void TakeOff()
    {
            _currentTime += Time.deltaTime;
            _mover.GetComponentInParent<Transform>().localPosition = Vector3.Lerp(_currentPosition, new Vector3(0,7,0), _moveCurve.Evaluate(_currentTime / _landingTime));  
            _mover.GetComponentInParent<Transform>().localRotation = Quaternion.Lerp(_currentRotation, _landingPoint.localRotation, _moveCurve.Evaluate(_currentTime / _landingTime)); 
    }   
    

}
