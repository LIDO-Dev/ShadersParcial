using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BloodVFX : MonoBehaviour
{
    private VisualEffect _bloodVfx;
    public KeyCode _interactKey = KeyCode.E;

    public void Awake()
    {
        _bloodVfx = GetComponentInChildren<VisualEffect>();  
    }

    public void SetEvent(string eventName)
    {
        _bloodVfx.SendEvent(eventName);
    }

        public void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            SetEvent("OnPlay");
        }
    }


}
