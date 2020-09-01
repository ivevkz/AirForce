using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField] private float _rotationRatio;
    [SerializeField] private float _rotationRatioStay;

    public float RotationRatio => _rotationRatio;
    public float RotationRatioStay => _rotationRatioStay;
}
