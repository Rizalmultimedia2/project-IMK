using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAble : MonoBehaviour
{
    private Renderer renderer;
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

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 1.2f && holdParent.transform.childCount == 0)
        {
            renderer.material.color = new Color32(56, 158, 39, 255);
        }
        else
        {
            renderer.material.color = new Color32(71, 218, 47, 255);
        }

        if (_controls.Player.PickUp.triggered)
        {
            if (holdParent.transform.childCount == 0)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < 1.2f)
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

    public override bool Equals(object obj)
    {
        return obj is PickUpAble able &&
               base.Equals(obj) &&
               EqualityComparer<Renderer>.Default.Equals(renderer, able.renderer);
    }
}
