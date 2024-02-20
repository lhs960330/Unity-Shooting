using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    // CharacterController는 월드기준으로 움직인다. 유일하게 transform만 자기자신기준의 좌표를 가진다.
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] TwoBoneIKConstraint leftHand;
    [SerializeField] WeaponHolder weaponHolder;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed;
    private bool isWalk;

    private void Update()
    {
        Move();
        JumpMove();
    }

    private void Move()
    {
        // CharacterController은 Move함수로 움직인다. (월드 기준으로 움직여서 폐기)
        //controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if (isWalk)
        {
            controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);
            controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
            animator.SetFloat("XSpeed", moveDir.x * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("MoveSpeed", moveDir.magnitude * walkSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            // 걸을때와 뛸때를 만들어준다.
            controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
            controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            // 움직일때 애니메이션이 바꿔준다.
            animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed, 0.1f, Time.deltaTime);
        }


        //animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed);

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
    public void Fire()
    {
        // 총쏘는 로직 구현
        animator.SetTrigger("Fire");
        weaponHolder.Fire();
    }
    public void Reload()
    {
        // 재장전 구현
        animator.SetTrigger("Reload");

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
    private void OnWalks(InputValue value)
    {
        if (value.isPressed)
        {
            isWalk = true;

        }
        else
        {
            isWalk = false;
        }
    }
    private void OnFire(InputValue value)
    {
        Fire();
    }
    private void OnReload(InputValue value)
    {

        Reload();
        StartCoroutine(ReloadCoroutine());

    }
    IEnumerator ReloadCoroutine()
    {
        leftHand.weight = 0f;
        yield return new WaitForSeconds(3.3f);
        leftHand.weight = 1f;
    }

}
