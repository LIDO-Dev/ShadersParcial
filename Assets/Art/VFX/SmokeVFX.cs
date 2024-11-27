using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeVFX : MonoBehaviour
{
    private VisualEffect _smokeVfx;
    public KeyCode _interactKey = KeyCode.E;
    public KeyCode _stopKey = KeyCode.Q;

    public void Awake()
    {
        _smokeVfx = GetComponentInChildren<VisualEffect>();  
    }

    public void SetEvent(string eventName)
    {
        _smokeVfx.SendEvent(eventName);
    }

        public void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            SetEvent("OnPlay");
        }

        if (Input.GetKeyDown(_stopKey))
        {
            SetEvent("OnStop");
        }
    }


}
