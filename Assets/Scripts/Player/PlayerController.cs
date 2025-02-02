using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPun
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float dashSpeed = 10f;
    
    [Header("Look")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 80f;
    [SerializeField] private float minVerticalAngle = -50f;
    [SerializeField] private float maxVerticalAngle = 50f;
    [SerializeField] private Camera playerCamera;
    
    public PhotonView photonView;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private bool isGrounded;
    private float verticalLookRotation;
    private bool isDashing;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (!photonView.IsMine)
        {
            playerCamera = GetComponentInChildren<Camera>();
            Destroy(playerCamera.gameObject);
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Moving()
    {
        if(!photonView.IsMine) return;
        // 카메라 방향을 기준으로 이동 벡터 계산
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        // y축은 제외하고 평면으로만 이동
        forward.y = 0f;
        right.y = 0f;

        // 이동 벡터는 카메라 방향을 기준으로 계산
        Vector3 move = forward * moveInput.y + right * moveInput.x;
        move.Normalize(); // 벡터를 정규화하여 일정한 속도로 유지

        float currentSpeed = isDashing ? dashSpeed : moveSpeed;
        characterController.Move(move * currentSpeed * Time.deltaTime); 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 점프 초기 속도^2 = 2 * g * h
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isGrounded)
        {
            isDashing = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isDashing = false;
        }
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            var ui = UIManager.Instance.Get<UIInventory>();
            if (ui != null)
            {
                UIManager.Instance.ToggleUI(ui);
            }
            else
            {
                UIManager.Instance.Show<UIInventory>();
            }
        }
    }
    

    private void HandleCameraRotation()
    {
        if(!photonView.IsMine) return;
        // 마우스 입력 기반 회전 계산
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 캐릭터 회전 (좌우)
        transform.Rotate(Vector3.up * mouseX);

        // 카메라 상하 회전 (클램프 처리)
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    private void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            Debug.Log($"포톤 연결상태: {PhotonNetwork.IsConnected}");
            return;
        }
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // !ground방지
            velocity.y = -2f;
        }
        
        Moving();
        
        // 중력 작용
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
        HandleCameraRotation();
    }
}
