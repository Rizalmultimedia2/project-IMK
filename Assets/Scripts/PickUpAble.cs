using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAble : MonoBehaviour
{
    public float pickUpRange = 5;
    public float moveForce = 150;
    private GameObject heldObject;
    public Transform dropPlace;
    public Transform player;
    private Controls _controls;
    public Transform holdParent;

    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controls.Player.PickUp.triggered)
        {
            if (heldObject == null)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < 1.5f)
                {
                    PickupObject(this.transform.gameObject);
                    Debug.Log("GRAB");
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.freezeRotation = true;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            heldObject = pickObj;
        }
    }

    void DropObject()
    {
        if (Vector3.Distance(player.transform.position, dropPlace.transform.position) < 1f)
        {
            Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();
            heldRig.transform.parent = dropPlace;
            heldRig.transform.position = dropPlace.position;
            heldRig.useGravity = true;
            heldRig.drag = 1;
            heldRig.freezeRotation = false;

            heldObject = null;
        }

    }
}
