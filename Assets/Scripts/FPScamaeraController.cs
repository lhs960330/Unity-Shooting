using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPScamaeraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;

    private Vector2 inputDir;
    private float xRotation;

    private void OnEnable()
    {
        // ���콺�� ����� ������ ���ڸ��� �ְ�����
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        // ����� ����� ������
        Cursor.lockState = CursorLockMode.None;
        // CursorLockMode.Confined�� ���� â���� ���콺�� �ȳ���������
    }
    private void Update()
    {

        xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
        // Clamp�� �ּڰ��� �ִ��� �ִ� �Լ��̴�.
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.Rotate(Vector3.up, mouseSensitivity * inputDir.x * Time.deltaTime);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //cameraRoot.Rotate(Vector3.right, mouseSensitivity*(-inputDir.y) * Time.deltaTime); 
    }

    private void OnLook(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
}
