using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Thief : BaseCharacter
{
    public override float MoveSpeed => 5.2f;
    public override float DashSpeed => 9f;
    private CharacterController controller;
    private bool isInprison = false;
    private float detectDistance = 3f;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask trapLayer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!photonView.IsMine) return;
        if (IsCollision(trapLayer, hit.collider.gameObject))
        {
            Debug.Log("트랩에 걸렸습니다.");
            StartCoroutine(WaitTrapTime());
        }
    }

    private IEnumerator WaitTrapTime()
    {
        controller.enabled = false;
        yield return new WaitForSeconds(3f);
        controller.enabled = true;
    }

    public override void FirstSkill()
    {
        // 첫 번째 스킬, 불: 주변에 불을 확산시키고, 경찰들이 제한시간내에 불을 끄지 못하면 패배
        // TODO: 카운트다운 시작 및 불 확산
        //GameObject fireObj = ResourceManager.Instance.LoadAsset<GameObject>("Fire",eAssetType.Prefab);

        GameObject obj = PhotonNetwork.Instantiate("Prefab/Fire", transform.position + new Vector3(0, 0, 1), Quaternion.identity);

        Debug.Log(" 도둑 스킬1");
    }

    public override void SecondSkill()
    {
        // TODO: 4가지 미션 아이템 수집하면 도둑 승리 로직 구현하기
        // 아이템 획득 스킬
        DetectItem();
        Debug.Log(" 도둑 스킬2");
    }

    private void DetectItem()
    {
        RaycastHit hit;
        Vector3 ray = transform.position + new Vector3(0, 1, 0);
        if (Physics.Raycast(ray, Vector3.forward, out hit, detectDistance, itemLayer))
        {
            Debug.Log("아이템 감지됌.");
            Destroy(hit.collider.gameObject);
            //Inventory.Instance.AddItem();
        }
    }

    private void CheckMissonItem()
    {
        if (Inventory.Instance.items.Count == 4)
        {
            GameManager.Instance.CheakGameEnd();
        }
    }

    [PunRPC]
    public void SyncPrisonPosition(Vector3 position) //위치 동기화
    {
        Debug.Log("동기화 실행감옥");
        transform.position = position;
        isInprison = true;
        GameManager.Instance.CheakGameEnd();
    }

    public bool IsPrison()
    {
        return isInprison;
    }
}
