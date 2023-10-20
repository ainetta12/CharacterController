using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShoulderTPS : MonoBehaviour
{
    private CharacterController _controller;
    Transform _camera;
    

    Transform _lookAtTransform;
    private float _horizontal;
    private float _vertical;


    //variable para saltar y gravedad

    [SerializeField] private float playerSpeed = 5; 
    [SerializeField] private float _jumpHeight = 1;

    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    //Variables para rotacion

     private float turnSmoothVeloity;

    [SerializeField] float turnSmoothTime = 0.1f;

    //variable para sensor

    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    [SerializeField] private AxisState xAxis;
    [SerializeField] private AxisState yAxis;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
        _lookAtTransform = GameObject.Find("LookAt").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement();
        Jump();
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
       
        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = 0;
        }

        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        _playerGravity.y += _gravity * Time.deltaTime;

        _controller.Move(_playerGravity * Time.deltaTime);

    }

    void Movement ()
    {
        Vector3 move = new Vector3(_horizontal,0,_vertical).normalized;

        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, xAxis.Value,0);
        _lookAtTransform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        if(move !=  Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        
    }
}
