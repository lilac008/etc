using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public void OnFirstHoverEntered() ///on은 이벤트함수
    {
        Debug.Log($"{gameObject.name} - OnFirstHoverEntered");
    }

    public void OnLastHoverExited() ///만들어낸 함수
    {
        Debug.Log($"{gameObject.name} - OnLastHoverExited");
    }

    public void OnHoverEntered() 
    {
        Debug.Log($"{gameObject.name} -  OnHoverEntered");
    }

    public void OnHoverExited()
    {
        Debug.Log($"{gameObject.name} -  OnHoverExited");
    }

    public void OnFirstSelectEntered()
    {
        Debug.Log($"{gameObject.name} - OnFirstSelectEntered");
    }

    public void OnLastSelectExited()
    {
        Debug.Log($"{gameObject.name} - OnLastSelectExited");
    }

    public void OnSelectEntered()
    {
        Debug.Log($"{gameObject.name} - OnSelectEntered");
    }

    public void OnSelectExited()
    {
        Debug.Log($"{gameObject.name} - OnSelectExited");
    }

    public void OnActivated()
    {
        Debug.Log($"{gameObject.name} - OnActivated");
    }

    public void OnDeactivated()
    {
        Debug.Log($"{gameObject.name} - OnDeactivated");
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
