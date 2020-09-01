using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class ControlPropeller : InputControl
{
    [Header("Винты")]
    [SerializeField] private GameObject _mainPropeller;
    [SerializeField] private GameObject _tailPropeller;

    private Mover _mover;
    private float _revs;
    private float _direction;
    private float _ratioCursor;

    private float _mainRotationRatioStay;
    private float _mainRotationRatio;

    private float _tailRotationRatioStay;
    private float _tailRotationRatio;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        _mainRotationRatio = _mainPropeller.GetComponent<Propeller>().RotationRatio;
        _mainRotationRatioStay = _mainPropeller.GetComponent<Propeller>().RotationRatioStay;

        _tailRotationRatio = _tailPropeller.GetComponent<Propeller>().RotationRatio;
        _tailRotationRatioStay = _tailPropeller.GetComponent<Propeller>().RotationRatioStay;


    }


    private void OnEnable()
    {
        _mover.ChangeCursor += ChangeInRotationRatio;
    }

    private void OnDisable()
    {
        _mover.ChangeCursor -= ChangeInRotationRatio;
    }

    private void Update()
    {
        if (!_mover.BoolIsProgress)
            base.HadleInputs();

        _mainPropeller.transform.Rotate(RotationPropellerMain(Vertical, _mover.IdleSpeed, _mover.CurrentForwardSpeed, _mover.BackwardSpeed));
        _tailPropeller.transform.Rotate(RotationPropellerTail(Horizontal, _mover.IdleSpeed, _mover.CurrentTiltSpeed, _mover.CurrentTiltSpeed));
    }

    private Vector3 RotationPropellerMain(float _direction, float idle, float forwardOrRight, float backwardOrLeft)
    {
        float calm = idle * _mainRotationRatioStay;

        if (_direction > 0)
            _revs = forwardOrRight * _mainRotationRatio;
        else
            _revs = backwardOrLeft * _mainRotationRatio;

        _revs = Mathf.Lerp(calm, _revs, Mathf.Abs(_direction));

        return Vector3.up * _revs * Time.deltaTime;
    }

    private Vector3 RotationPropellerTail(float _direction, float idle, float forwardOrRight, float backwardOrLeft)
    {
        float calm = idle * _tailRotationRatioStay;

        if (_direction > 0)
            _revs = forwardOrRight * _tailRotationRatio;
        else
            _revs = backwardOrLeft * _tailRotationRatio;

        _revs = Mathf.Lerp(calm, _revs, Mathf.Abs(_direction));
        return Vector3.left * (_revs * _ratioCursor) * Time.deltaTime;
    }

    private void ChangeInRotationRatio(float ratioCursor)
    {
        _ratioCursor = ratioCursor;
    }
}
