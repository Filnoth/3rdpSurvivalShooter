using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [Header("Character Controller Settings")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityValue;

    private Vector3 _direction;
    private Vector3 _velocity;

    private Camera _mainCamera;

    [Header("Camera Control Settings")]
    [SerializeField]
    private float _sensitivity = 10f;
    private float _yVelocity;

    

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("Character controller is null");
        }

        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main camera is null");
        }

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        Movement();
        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void Movement()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);
        Vector3 velocity = direction * _speed;

        velocity = transform.TransformDirection(velocity);

        if (_controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravityValue;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        //Move side to side
        transform.Rotate(0, mouseX, 0);

        //move Up and Down
        Vector3 cameraVert = _mainCamera.gameObject.transform.localEulerAngles;
        cameraVert.x -= mouseY;
        cameraVert.x = Mathf.Clamp(cameraVert.x, 0, 24);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(cameraVert.x, Vector3.right);
    }
}
