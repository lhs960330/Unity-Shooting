using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    // CharacterController�� ����������� �����δ�. �����ϰ� transform�� �ڱ��ڽű����� ��ǥ�� ������.
    [SerializeField] CharacterController controller;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed;

    private void Update()
    {
        Move();
        JumpMove();
    }

    private void Move()
    {
        // CharacterController�� Move�Լ��� �����δ�. (���� �������� �������� ���)
        //controller.Move(moveDir * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
    }
    private void JumpMove()
    {
        // �߷°��ӵ��� �������ش�. deltaTime�� �ð� gravity.y �Ÿ�
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // ������ �������� �߷°��ӵ��� �ö󰡴� ������ ������ ����
        // controller.isGrounded ���ٴڿ� ������
        // isGrounded ���� ������
        if (controller.isGrounded)
        {
            ySpeed = 0f;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }

    private void OnJump(InputValue value)
    {
        ySpeed = jumpSpeed;
    }

}
