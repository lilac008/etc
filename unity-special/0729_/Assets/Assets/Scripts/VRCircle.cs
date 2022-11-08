using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //

public class VRCircle : MonoBehaviour
{

    public Image imgCircle;
    public float totalTime = 2.0f;

    bool gvrStatus;
    float gvrTimer;

    


    void Update()
    {

        if (gvrStatus) 
        {
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 10)) 
        {
            if (imgCircle.fillAmount == 1 && hit.transform.CompareTag("Teleport")) 
            {
                hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
            }
        }
    }



    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgCircle.fillAmount = 0;
    }


}

