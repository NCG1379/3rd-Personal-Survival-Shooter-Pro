using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [Header("Controller Settings")]
    
    [SerializeField]
    private float _speed = 6.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;

    Vector3 _direction;
    Vector3 _velocity;

    private Camera _mainCamera;
    [Header("Camera Settings")]
    public float mouseX;
    public float mouseY;
    [SerializeField]
    float _XSensivity = 4.0f;
    [SerializeField]
    float _YSensivity = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
            Debug.LogError("_controller is NULL");

        _mainCamera = Camera.main;

        if (_mainCamera == null)
            Debug.LogError("__mainCamera is null");

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CameraController(_XSensivity, _YSensivity);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Movement()
    {
        if (_controller.isGrounded == true)
        {
            _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _direction = transform.TransformDirection(_direction); //Fixed problem
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;
        
        //Apply transform.TransformDirection(_velocity) create an issue related with the jumping behaviour

        _controller.Move(_velocity * Time.deltaTime);
    }

    void CameraController(float XSensivity, float YSensivity)
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Look left and Right
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.y += mouseX * XSensivity;
        transform.localRotation = Quaternion.AngleAxis(currentRot.y, Vector3.up);
        

        //Look Up and Down
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * YSensivity;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 5, 26);
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }


}
