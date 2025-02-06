using Photon.Pun;
using UnityEngine;

public class Elevator : MonoBehaviourPun
{
    [SerializeField] private float elevatorSpeed = 5f;
    [SerializeField] private float elevatorRange = 13f;

    private float currentDistance = 0f;
    private float targetDistance = 0f;
    private int currentDirection = 0;
    private bool isMoving = false;
    private Vector3 initialPosition; // 초기 위치 저장

    private void Start()
    {
        initialPosition = transform.position; // 초기 위치 저장
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveElevator();
        }
    }

    public void OnLift()
    {
        if (!isMoving) // 이미 움직이는 중이면 중복 호출 방지
        {
            targetDistance = Mathf.Min(elevatorRange, initialPosition.y + elevatorRange - transform.position.y); // 최대 높이 제한
            currentDirection = 1;
            isMoving = true;
            photonView.RPC("SyncElevator", RpcTarget.All, targetDistance, currentDirection); // RPC 호출
        }
    }

    public void OnDown()
    {
        if (!isMoving) // 이미 움직이는 중이면 중복 호출 방지
        {
            targetDistance = Mathf.Min(elevatorRange, transform.position.y - initialPosition.y); // 최대 내려갈 거리 제한
            currentDirection = -1;
            isMoving = true;
            photonView.RPC("SyncElevator", RpcTarget.All, targetDistance, currentDirection); // RPC 호출
        }
    }
    
    [PunRPC]
    private void SyncElevator(float targetDistance, int currentDirection)
    {
        this.targetDistance = targetDistance;
        this.currentDirection = currentDirection;
        isMoving = true;
        currentDistance = 0;

    }

    private void MoveElevator()
    {
        float distanceToMove = elevatorSpeed * Time.deltaTime;

        if (currentDirection == 1) // 위로 이동
        {
            if (currentDistance + distanceToMove > targetDistance)
            {
                distanceToMove = targetDistance - currentDistance;
            }
        }
        else // 아래로 이동
        {
            if (currentDistance + distanceToMove > targetDistance)
            {
                distanceToMove = targetDistance - currentDistance;
            }
        }

        transform.Translate(Vector3.up * currentDirection * distanceToMove);
        currentDistance += distanceToMove;

        if (currentDistance >= targetDistance)
        {
            isMoving = false;
            currentDistance = 0f;
        }
    }
}