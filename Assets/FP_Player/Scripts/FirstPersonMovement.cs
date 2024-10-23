using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("<color=green>Running</color>")]
    public bool canRun = true;
    public float speed = 5;
    public bool _isRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode _runningKey = KeyCode.LeftShift;
    public KeyCode _interactKey = KeyCode.E;
    public Camera _camera;
    [SerializeField] private float _intRayDist = 2.0f;
    [SerializeField] private LayerMask _intMask;

    [Header("<color=yellow>Animator</color>")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _xAxisName = "xAxis";
    [SerializeField] private string _zAxisName = "zAxis";
    [SerializeField] private string _isMoving = "Moving";
    private float _xAxis, _zAxis;
    private Vector3 _dir;

    [Header("<color=purple>Audio</color>")]
    [SerializeField] private AudioClip[] _stepClips;
    [SerializeField] private AudioClip[] _jumpClips;
    [SerializeField] private AudioClip[] _landClips;

    private Ray _intRay, _movRay;
    private RaycastHit _intHit;

    private AudioSource _source;
    new Rigidbody rigidbody;
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
        _isRunning = canRun && Input.GetKey(_runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = _isRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        if(_isRunning == true)
        {
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
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

        if (Input.GetKeyDown(_interactKey))
        {
            Interact();
        }
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

    public void PlayJumpClip()
    {
        if (_source.isPlaying)
        {
            _source.Stop();
        }

        _source.clip = _jumpClips[Random.Range(0, _jumpClips.Length)];

        _source.Play();
    }  

    public void PlayLandClip()
    {
        if (_source.isPlaying)
        {
            _source.Stop();
        }

        _source.clip = _landClips[Random.Range(0, _landClips.Length)];

        _source.Play();
    } 

    private void Interact()
    {
        _intRay = new Ray(_camera.transform.position, _camera.transform.forward);

        if(Physics.SphereCast(_intRay, 0.25f, out _intHit, _intRayDist, _intMask))
        {
            if(_intHit.collider.TryGetComponent(out IInteract intObj))
            {
                intObj.OnInteract();
            }
        }
    }     
}