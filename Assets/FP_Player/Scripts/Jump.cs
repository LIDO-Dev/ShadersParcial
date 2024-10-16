using UnityEngine;

public class Jump : MonoBehaviour
{
    new Rigidbody rigidbody;
    [Header("<color=blue>Jumping</color>")]
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;
    Crouch crouch;
    private Animator _animator;
    [SerializeField] private string _onJumpName = "onJump";
    public bool canJump;

    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
        crouch = GetComponentInChildren<Crouch>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        canJump = true;
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded) && canJump == true)
        {
            //rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            //Jumped?.Invoke();
            _animator.SetTrigger(_onJumpName);
        }

    }

    public void Jumping()
    {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
    }
}
