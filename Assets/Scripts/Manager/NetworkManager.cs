using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //public Text StatusText;
    //public InputField NickNameInput;
    public string gameVersion = "1.0";


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 성공");
        GameManager.Instance.SpawnManager.Initialize(GameManager.Instance.SelectPrefabName);
        //PhotonNetwork.Instantiate("Police", spawnPos.transform.position, Quaternion.identity);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("랜덤 방 입장 실패");
        this.CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("새로운 방을 생성합니다.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });
    }
}
