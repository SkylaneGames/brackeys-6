using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponLoadout))]
public class MechController : MonoBehaviour
{
    private CharacterController _characterController;
    private WeaponLoadout _weapons;

    [SerializeField]
    [Range(0f, 50f)]
    private float _speed = 5f;

    [SerializeField]
    [Range(0f, 5f)]
    private float _turnSpeed = 1f;

    private Vector2 _positionInput = Vector2.zero;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _weapons = GetComponent<WeaponLoadout>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        var move = transform.forward * _positionInput.y * _speed;
        move.y = -9.81f;
        transform.Rotate(Vector3.up, _positionInput.x * _turnSpeed, Space.World);
        _characterController.Move(move * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _positionInput = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Firing weapon");
            _weapons.FireWeapon(0);
        }
        else if (context.canceled)
        {
            Debug.Log($"Stopped firing weapon");
            _weapons.StopFiringWeapon(0);
        }
    }
}
