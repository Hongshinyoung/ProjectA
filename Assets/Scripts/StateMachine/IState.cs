using UnityEngine;

public interface IState
{
    // 상태 진입
    public void Enter();
    // 매 프레임 상태 업데이트
    public void Update();
    // 상태 종료
    public void Exit();
}
