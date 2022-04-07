using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    CharacterController _controller;
    AnimationController _animationController;
    Camera _mainCam;
    public Joystick _joystick;
    
    [Header("Movement Values")]
    float movSpeed = 5;
    float rotationSpeed = 720;
    float horizontalInput;
    float verticalInput;
    Vector3 movDirection;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animationController = GetComponent<AnimationController>();
        _mainCam = Camera.main;
    }

    void Update()
    {

        horizontalInput = _joystick.Horizontal; 
        verticalInput = _joystick.Vertical; 
        movDirection = new Vector3(horizontalInput, 0, verticalInput);
        movDirection.Normalize(); 

        if (movDirection != Vector3.zero)
        {
          
            Quaternion toRotation = Quaternion.LookRotation(movDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed*Time.deltaTime);

            _animationController._animator.SetBool("IsRunning", true); 

        }
        else
        {
            _animationController._animator.SetBool("IsRunning", false);
        }
        
        _controller.SimpleMove(movDirection * movSpeed);
    }
    private void LateUpdate()
    {
        _mainCam.transform.position = Vector3.Lerp(_mainCam.transform.position,new Vector3(transform.position.x, transform.position.y + 11f, transform.position.z-13.39f), 3*Time.deltaTime);
    }
}
