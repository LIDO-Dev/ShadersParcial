using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuntions : MonoBehaviour
{
    private FirstPersonMovement _parent;

    private void Start()
    {
        _parent = GetComponentInParent<FirstPersonMovement>();
    }

    public void PlayStepClip()
    {
        _parent.PlayStepClip();
    }
}
