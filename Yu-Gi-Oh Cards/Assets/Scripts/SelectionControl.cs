using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionControl : MonoBehaviour
{
    public static SelectionControl instance;

    private bool _mouseState;
    private bool _mouseStateSelection;
    public GameObject target;
    public GameObject targetSelection;
    private Vector3 screenSpace;
    private Vector3 offset;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        #region Moving selected card
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            RaycastHit _hit;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(_ray, out _hit))
                {
                    if (_hit.transform.tag == "CardOn")                 
                        target = _hit.transform.gameObject; 

                    if (_hit.transform.tag == "CardOff")
                        targetSelection = _hit.transform.gameObject;
                }
            }

            if (target != null)
            {
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            }

            if (targetSelection != null)
            {
                _mouseStateSelection = true;
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            }
        }
        #endregion

        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
            _mouseStateSelection = false;
        }
        if (_mouseState)
        {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            target.transform.position = curPosition;
        }

        if (_mouseStateSelection)
        {
            targetSelection.GetComponent<BoxCollider>().enabled = true;
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            targetSelection.transform.position = curPosition;
        }
        else targetSelection.GetComponent<BoxCollider>().enabled = false;


    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

    // function written so that if raycast hit UI it won't do anything in the game
    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

