using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    public PlayerController Player { get; }

    public PlayerIdleState IdleState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(PlayerController player) : base(player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.PlayerData.GroundData.BaseSpeed;
        RotationDamping = player.PlayerData.GroundData.BaseRotationDamping;
    }

}
