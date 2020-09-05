using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputControl : MonoBehaviour
{
    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    public float HorizonalMouse { get; private set; }
    public float VerticalMouse { get; private set; }

    public event UnityAction PressShift;
    public event UnityAction<bool> StartAction;
    public event UnityAction FireRocket;
    public event UnityAction FireMachineGun;

    private void Update()
    {
        HadleInputs();
    }

    protected void HadleInputs()
    {   
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");

        HorizonalMouse = Input.GetAxis("Mouse X");
        VerticalMouse = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.LeftShift))
            PressShift?.Invoke();

        if (Input.GetKey(KeyCode.E))        
            StartAction?.Invoke(true);

        if (Input.GetMouseButton(0))
            FireMachineGun?.Invoke();

        if (Input.GetMouseButton(1))
            FireRocket?.Invoke();

        
    }
}
