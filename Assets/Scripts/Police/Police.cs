using Photon.Pun;
using UnityEngine;

public class Police : BaseCharacter
{
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask elevatorLayer;
    [SerializeField] private Transform prisonPosition;
    [SerializeField] private LayerMask fireLayer;
    [SerializeField] private float fireDetectDistance = 3f;
    private CharacterController controller;

    public override float MoveSpeed => 5f;

    public override float DashSpeed => 10f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        var prison = GameObject.Find("Prison(Clone)");
        prisonPosition = prison.transform;
    }

    public override void FirstSkill()
    {
        if (!photonView.IsMine) return;
        TryDetectFire();
        Debug.Log("스킬1");
    }

    public override void SecondSkill()
    {
        // 트랩 설치하기
        GameObject trap = ResourceManager.Instance.LoadAsset<GameObject>("Trap", eAssetType.Prefab);
        if( trap != null)
        {
            Instantiate(trap, transform.position, Quaternion.identity);
            Debug.Log("트랩 설치");
        }
        Debug.Log("스킬2");
    }

    private void Update()
    {
        //if (!photonView.IsMine) return;

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    // 불 탐지
        //    TryDetectFire();
        //}
    }

    private void TryDetectFire()
    {
        RaycastHit hit;
        Vector3 ray = transform.position + Vector3.up * 1.0f; // 눈 높이

        if (Physics.Raycast(ray, transform.forward, out hit, fireDetectDistance, fireLayer))
        {
            Debug.Log("불이 탐지됨");
            PhotonView fireView = hit.collider.GetComponent<PhotonView>();
            if (fireView != null)
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(hit.collider.gameObject);
                }
                else
                {
                    photonView.RPC("DestroyFire", RpcTarget.MasterClient, fireView.ViewID);
                }
            }
        }
        else
        {
            Debug.Log("불이 탐지되지 않음");
        }
    }

    [PunRPC]
    private void DestroyFire(int viewID)
    {
        PhotonView fireView = PhotonView.Find(viewID);
        if (fireView != null)
        {
            PhotonNetwork.Destroy(fireView.gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!photonView.IsMine) return;
        if (IsCollision(detectLayer, hit.collider.gameObject))
        {
            Thief thiefController = hit.collider.GetComponent<Thief>();
            if (thiefController != null)
            {
                PhotonView theifView = thiefController.GetComponent<PhotonView>();
                if (theifView != null)
                {
                    theifView.RPC("SyncPrisonPosition", RpcTarget.All, prisonPosition.position + new Vector3(0, 2, 0));
                    Debug.Log("도둑 감옥으로 이동");
                }
            }
        }
        if ((1 << hit.collider.gameObject.layer & elevatorLayer) != 0)
        {
            Elevator elevator = hit.collider.gameObject.GetComponent<Elevator>();
            if (elevator != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    elevator.OnDown();
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    elevator.OnLift();
                }
            }
        }
        if (IsCollision(fireLayer, hit.collider.gameObject))
        {
            Debug.Log("불이 탐지됨");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("불 끄기");
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
