using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("BadCircle") || raycastHit.collider.CompareTag("GoodCircle") || raycastHit.collider.CompareTag("FastCircle"))
                {
                    raycastHit.collider.gameObject.GetComponent<Circle>().DestroyCircle();
                    if (raycastHit.collider.CompareTag("BadCircle")) GameManager.i.GameOver();
                }
            }
        }
    }
}
