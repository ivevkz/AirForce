using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Player))]
public class Mover : InputControl
{
    [SerializeField] private Transform _transformHeli;
    [SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private float _maxTiltForwardBackward = 20;
    [SerializeField] private float _maxTiltAngleLeftToRight = 30;
    [SerializeField] private bool _isGround;

    public event UnityAction<float> ChangeCursor;

    private Helicopter _helicopter;
    private float _backwardSpeed;
    private float _tiltSpeed;
    private float _idleSpeed;

    private float _tiltForwardBackward;
    private float _tiltLeftToRight;
    private float _currentForwardSpeed;
    private float _currentTiltSpeed;
    private float _currentGroundDistance;
    private bool _acceleration = false;
    private bool _boolIsProgress = false;

    private Vector3 _moveVector;
    private Rigidbody _rigidbody;
    public float IdleSpeed => _idleSpeed;
    public float CurrentForwardSpeed => _currentForwardSpeed;
    public float CurrentTiltSpeed => _currentTiltSpeed;
    public float BackwardSpeed => _backwardSpeed;
    public bool BoolIsProgress => _boolIsProgress;
    public bool IsGround => _isGround;


    private void OnEnable()
    {
        PressShift += Acceleration;
        StartAction += IsProgress;
    }

    private void OnDisable()
    {
        PressShift -= Acceleration;
        StartAction -= IsProgress;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _helicopter = GetComponent<Helicopter>();
         
        _currentForwardSpeed = _helicopter.EnginePower;
        _backwardSpeed = _helicopter.EnginePower / 1.1f;
        _tiltSpeed = _helicopter.EnginePower / 1.5f;
        _idleSpeed = _helicopter.EnginePower / 2.5f;
    }

    private void Update()
    {
        if (!_boolIsProgress)
        {
            base.HadleInputs();
            GroundDistance();

            if (!_isGround)
            {
                MoverHelicopter();
                RotateHelicopter();
                LookOnCursor();
            }
        }
        else
        {
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_moveVector);
    }
    private void MoverHelicopter()
    {
        if (!_acceleration)
        {
            _currentForwardSpeed = _helicopter.EnginePower;
            _currentTiltSpeed = _tiltSpeed;
        }

        float moveX, moveZ;
        moveX = Horizontal * _currentTiltSpeed;

        if (Vertical > 0)
            moveZ = Vertical * _currentForwardSpeed;
        else
            moveZ = Vertical * _backwardSpeed;

        _moveVector = new Vector3(moveX, 0, moveZ);
        _acceleration = false;
    }
    private void Acceleration()
    {
        _acceleration = true;
        _currentForwardSpeed = _helicopter.EnginePower * 1.7f;
        _currentTiltSpeed = _tiltSpeed * 1.7f;
    }
    private void RotateHelicopter()
    {
        if (Mathf.Abs(Vertical) > 0)
            _tiltForwardBackward = Vertical * _maxTiltForwardBackward;
        else
            _tiltForwardBackward = 0;

        if (Mathf.Abs(Horizontal) > 0)
            _tiltLeftToRight = Horizontal * -_maxTiltAngleLeftToRight;
        else
            _tiltLeftToRight = 0;

        Quaternion rotationTarget = Quaternion.Euler(_tiltForwardBackward, 0, _tiltLeftToRight);
        Quaternion rotation = Quaternion.Lerp(_transformHeli.transform.localRotation, rotationTarget, Time.deltaTime * 2);

        _transformHeli.transform.localRotation = rotation;
    }
    private void LookOnCursor()
    {
        Plane player = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hit = 0;
        if (player.Raycast(ray, out hit))
        {
            Vector3 targetPoint = ray.GetPoint(hit);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            if (Mathf.Abs(HorizonalMouse) > 0 || Mathf.Abs(VerticalMouse) > 0)
                ChangeCursor?.Invoke(2f);
            else
                ChangeCursor?.Invoke(1f);
        }
    }
    private void GroundDistance()
    {
        Ray hoverRay = new Ray(_transformHeli.position, Vector3.down);
        Debug.DrawRay(_transformHeli.position, hoverRay.direction, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(hoverRay, out hit, 100f))
        {
            if (hit.transform.tag == "ground")
            {
                _currentGroundDistance = hit.distance;
                if (_currentGroundDistance > 2)
                    _isGround = false;
                else
                    _isGround = true;
            }
        }
    }
    public void IsProgress(bool progress)
    {
        _boolIsProgress = progress;
    }
}
