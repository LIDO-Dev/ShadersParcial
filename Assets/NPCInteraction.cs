using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteract
{
    [Header("<color=#7B2FBC>Animation</color>")]
    [SerializeField] private string _interactionName = "Interact";
    public Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnInteract()
    {
        _anim.SetTrigger(_interactionName);
        Debug.Log("Interaction");
    }
}
