using UnityEngine;

public class HeadFollower : MonoBehaviour
{
    public Transform target; // El objeto del cual copiar la posición Y

    void Update()
    {
        if (target != null)
        {
            Vector3 newPosition = transform.position; // Obtiene la posición actual del objeto
            newPosition.y = target.position.y; // Copia solo la posición Y del objeto objetivo
            transform.position = newPosition; // Asigna la nueva posición al objeto
        }
    }
}

