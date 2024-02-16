using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �� ������Ʈ�� Player���� �����ǰ� �ڽ� ������Ʈ�� cameraRoot�� ������ ī�޶� cameraRoot ������Ʈ�� �ٶ󺸰��Ͽ� ������ �����δ�.?
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

    // LateUpdate �� ��� �����Ȳ�� �������� ����ȴ�. ��ġ���� ���������� �����Ͽ� �� ��
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
