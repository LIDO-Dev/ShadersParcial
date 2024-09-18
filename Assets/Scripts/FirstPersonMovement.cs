using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("<color=yellow>Animator</color>")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";
    [SerializeField] private string _isMoving = "Moving";
    private float _xAxis, _zAxis;
    private Vector3 _dir;

    [Header("<color=purple>Audio</color>")]
    [SerializeField] private AudioClip[] _stepClips;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    private AudioSource _source;
    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();

         if(!_animator)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        _source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    private void Update()
    {
        _animator.SetFloat(_xAxisName, _xAxis);
        _animator.SetFloat(_zAxisName, _zAxis);
        _animator.SetBool(_isMoving, (_xAxis != 0 || _zAxis != 0));

        _xAxis = Input.GetAxis($"Horizontal");
        _zAxis = Input.GetAxis($"Vertical");

        _dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;
    }

    public void PlayStepClip()
    {
        if (_source.isPlaying)
        {
            _source.Stop();
        }

        _source.clip = _stepClips[Random.Range(0, _stepClips.Length)];

        _source.Play();
    }
}