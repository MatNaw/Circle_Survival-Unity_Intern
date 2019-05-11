using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        /*if (!TouchThrough.IsPointerOverUIObject() && !EventSystem.current.IsPointerOverGameObject())
        {*/
            //Debug.Log("Not touch through && not overgameobject");
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                //Debug.Log("touchCount > 0 && lpm down");
                Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);//(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    //Debug.Log("Something Hit");

                    if (raycastHit.collider.CompareTag("BadCircle") || raycastHit.collider.CompareTag("GoodCircle"))
                    {
                        //Debug.Log("Circle clicked");
                        raycastHit.collider.gameObject.GetComponent<Circle>().DestroyCircle();
                        if (raycastHit.collider.CompareTag("BadCircle")) GameManager.i.GameOver();
                    }
                }
            }
        //}
    }
}
