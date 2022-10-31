using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //UnityEvent

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    private Magazine magazine;

    private void Awake()
    {
        magazine = GetComponent<Magazine>();
    }

    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
        
    }

    private IEnumerator Process() 
    {
        var wfs = new WaitForSeconds(shootDelay);

        while (true) 
        {
            Shoot();
            yield return wfs;
        }
    }

    private void Shoot() 
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            var hitObject = hitInfo.transform.GetComponent<Hittable>(); //hittable script에서 추가
            hitObject?.Hit();//

            OnShootSuccess?.Invoke(hitInfo.point);
        }
        else 
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitpoint);
        }
    }



}
