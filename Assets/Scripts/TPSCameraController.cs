using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 이 컴포넌트는 Player에서 생성되고 자식 오브젝트인 cameraRoot를 받으면 카메라가 cameraRoot 오브젝트를 바라보게하여 시점을 움직인다.?
public class TPSCameraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;
    [SerializeField] GameObject TPSCamera;

    private Vector2 inputDir;
    private float xRotation;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // LateUpdate 는 모든 진행상황이 진행한후 실행된다. 위치에서 마지막으로 실행하여 잘 됨
    private void LateUpdate()
    {
        xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.Rotate(Vector3.up, inputDir.x * mouseSensitivity * Time.deltaTime);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
    private void OnLook(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
    private void OnZoom(InputValue value)
    {
        if (value.isPressed)
        {
            TPSCamera.GetComponent<CinemachineVirtualCamera>().Priority = 9;
        }
        else
        {
            TPSCamera.GetComponent<CinemachineVirtualCamera>().Priority = 20;
        }
    }
}
