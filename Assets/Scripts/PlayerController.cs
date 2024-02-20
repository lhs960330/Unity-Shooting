using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    // CharacterController�� ����������� �����δ�. �����ϰ� transform�� �ڱ��ڽű����� ��ǥ�� ������.
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
        // CharacterController�� Move�Լ��� �����δ�. (���� �������� �������� ���)
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
            // �������� �۶��� ������ش�.
            controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
            controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            // �����϶� �ִϸ��̼��� �ٲ��ش�.
            animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed, 0.1f, Time.deltaTime);
        }


        //animator.SetFloat("MoveSpeed", moveDir.magnitude * moveSpeed);

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
    public void Fire()
    {
        // �ѽ�� ���� ����
        animator.SetTrigger("Fire");
        weaponHolder.Fire();
    }
    public void Reload()
    {
        // ������ ����
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
