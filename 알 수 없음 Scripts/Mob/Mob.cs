using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // NavMeshAgent
using UnityEngine.Events;   // UnityEvent

public class Mob : MonoBehaviour
{
    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    private bool isDestroyed = false;

    public float destroyDelay = 1f;


    private void Start()
    {
        Invoke(nameof(Destroy), 3f);

        OnCreated?.Invoke();

    }


    public void Destroy()
    {
       if(isDestroyed)
            return;

       isDestroyed = true;

        Destroy(gameObject, destroyDelay);

        OnDestroyed?.Invoke();
    }







}
