using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    // CharacterController는 월드기준으로 움직인다. 유일하게 transform만 자기자신기준의 좌표를 가진다.
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
        // CharacterController은 Move함수로 움직인다. (월드 기준으로 움직여서 폐기)
        //controller.Move(moveDir * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
    }
    private void JumpMove()
    {
        // 중력가속도를 구현해준다. deltaTime을 시간 gravity.y 거리
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // 가만히 있을때도 중력가속도가 올라가는 오류가 있으니 수정
        // controller.isGrounded 땅바닥에 있을때
        // isGrounded 별로 안좋음
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
