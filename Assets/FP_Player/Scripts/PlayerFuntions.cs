using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuntions : MonoBehaviour
{
    private Jump _jumpparent;
    private FirstPersonMovement _movementparent;

    private void Start()
    {
        _jumpparent = GetComponentInParent<Jump>();
        _movementparent = GetComponentInParent<FirstPersonMovement>();        
    }

    public void JumpVoid()
    {
        _jumpparent.Jumping();
    }

    public void PlayStepClip()
    {
        _movementparent.PlayStepClip();
    }

    public void PlayJumpClip()
    {
        _movementparent.PlayJumpClip();
    }

    public void PlayLandClip()
    {
        _movementparent.PlayLandClip();
    }
}
