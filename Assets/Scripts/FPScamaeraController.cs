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
        // 마우스가 가운데에 잡혀서 그자리에 있게해줌
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        // 가운데에 잡던걸 놓아줌
        Cursor.lockState = CursorLockMode.None;
        // CursorLockMode.Confined은 게임 창에서 마우스가 안나가게해줌
    }
    private void Update()
    {

        xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
        // Clamp는 최솟값과 최댓값을 주는 함수이다.
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
