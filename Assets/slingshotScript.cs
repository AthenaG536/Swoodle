using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshotScript : MonoBehaviour
{

    public Rigidbody projectile;
    public Transform slingshotAnchor;
    public float maxStretch = 10.0f;
    public float fireSpeed = 10.0f;
    
    public SpringJoint springJoint;
    private Vector3 slingshotToProjectileVector;
    private float sqrMaxStretch;



    public LineRenderer lr;
    // public Transform slingshotAnchor;
    // public Rigidbody projectile;
    // public float maxStretch = 3.0f;
    // public float shootSpeed = 10.0f;

    private float currentStretch;
    private bool isDragging = false;


        private void Start()
    {
        //
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == projectile.gameObject)
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = hit.point - slingshotAnchor.position;
                currentStretch = Mathf.Clamp(dir.magnitude, 0, maxStretch);
                lr.SetPosition(0, projectile.position);
                lr.SetPosition(1, projectile.position + dir.normalized * currentStretch);
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = hit.point - slingshotAnchor.position;
                projectile.velocity = dir.normalized * currentStretch * fireSpeed;
                lr.SetPosition(0, Vector3.zero);
                lr.SetPosition(1, Vector3.zero);
                isDragging = false;
                springJoint.breakForce = 0;
            }
        }

        if (Input.touchCount > 0 && isDragging)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 dir = hit.point - slingshotAnchor.position;
                currentStretch = Mathf.Clamp(dir.magnitude, 0, maxStretch);
                lr.SetPosition(0, projectile.position);
                lr.SetPosition(1, projectile.position + dir.normalized * currentStretch);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 dir = hit.point - projectile.position;
                    projectile.velocity = dir.normalized * currentStretch * fireSpeed;
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                    isDragging = false;
                }
            }
        }
    }



    void StartDrag()
    {
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        springJoint.breakForce = 0;
        slingshotToProjectileVector = projectile.position - slingshotAnchor.position;
        sqrMaxStretch = maxStretch * maxStretch;
        springJoint.connectedAnchor = slingshotAnchor.position;
    }

    void Fire()
    {
        // // -- Works kinda
        // projectile.GetComponent<Rigidbody>().isKinematic = false;
        // springJoint.breakForce = Mathf.Infinity;
        // projectile.GetComponent<Rigidbody>().velocity = -slingshotToProjectileVector * fireSpeed;
        // projectile = null;
        // projectile.GetComponent<Rigidbody>().isKinematic = false;
        
        Debug.Log("Fire has been triggered");

        projectile.GetComponent<Rigidbody>().isKinematic = false;
        springJoint.breakForce = Mathf.Infinity;

        // Calculate the direction to fire in
        Vector3 fireDirection = slingshotAnchor.position - projectile.position;

        // Reverse the direction to fire in
        fireDirection = -fireDirection;

        // Draw a line in the direction of the fired projectile
        Debug.DrawRay(projectile.position, fireDirection * 10f, Color.green, 2f);

        // Apply the force to the projectile's rigidbody
        projectile.GetComponent<Rigidbody>().AddForce(fireDirection * fireSpeed, ForceMode.Impulse);

        projectile = null;
    }
}
