using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshotScript : MonoBehaviour
{

    public Rigidbody projectile;
    public Transform slingshotAnchor;
    public float maxStretch;
    public float fireSpeed;
    private Vector3 initialClickPos;
    
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
        if(projectile && projectile.position.z >= 30 && projectile.position.z <= 32){

            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                Vector3 startPos = new Vector3(projectile.position.x, projectile.position.y, projectile.position.z);
                lr.SetPosition(0, startPos);
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                // Get the position of the mouse on screen
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 32f;

                currentStretch = Mathf.Clamp(mousePos.magnitude, 0, maxStretch);
                mousePos.z = 32f;

                // Set the end point of the line renderer to the mouse position in world space
                lr.SetPosition(1, mousePos);
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                Vector3 dir = lr.GetPosition(1) - lr.GetPosition(0);
                projectile.velocity = dir.normalized * currentStretch * fireSpeed;
                lr.SetPosition(0, Vector3.zero);
                lr.SetPosition(1, Vector3.zero);
                isDragging = false;
                if(springJoint) springJoint.breakForce = 0;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (touch.phase == TouchPhase.Began && Physics.Raycast(ray, out hit) && hit.transform == projectile)
                {
                    isDragging = true;
                    // Set the starting point of the line renderer to the touch position in world space
                    Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector3 touchPos = new Vector3(touchWorldPos.x, touchWorldPos.y, projectile.position.z);
                    lr.SetPosition(0, touchPos);
                }
                

                if (isDragging)
                {
                    // Get the position of the touch on screen
                    Vector3 touchPos = touch.position;
                    touchPos.z = Vector3.Distance(Camera.main.transform.position, slingshotAnchor.position);
                    // Convert the touch position to world space
                    Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(touchPos);
                    // worldTouchPos.z = 32f;

                    // currentStretch = Mathf.Clamp(worldTouchPos.magnitude, 0, maxStretch);
                    // worldTouchPos.z = 32f;


                    worldTouchPos.z = 32f;
                    // worldTouchPos.z = Mathf.Clamp(worldTouchPos.z, 0, maxStretch); // add this line to clamp the Z position
                    currentStretch = Mathf.Clamp(worldTouchPos.magnitude, 0, maxStretch);

                    lr.SetPosition(1, worldTouchPos);
                }

                if (touch.phase == TouchPhase.Ended && isDragging)
                {
                    Vector3 dir = lr.GetPosition(1) - lr.GetPosition(0);
                    projectile.velocity = dir.normalized * currentStretch * fireSpeed;

                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                    isDragging = false;
                    if(springJoint) springJoint.breakForce = 0;
                }
            }
        }

    }



    void StartDrag()
    {
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        springJoint.breakForce = 0;
        sqrMaxStretch = maxStretch * maxStretch;
        springJoint.connectedAnchor = slingshotAnchor.position;
    }

    void Fire()
    {
        
        Debug.Log("Fire has been triggered");

        projectile.GetComponent<Rigidbody>().isKinematic = false;
        springJoint.breakForce = Mathf.Infinity;

        // Calculate the direction to fire in
        Vector3 fireDirection = slingshotAnchor.position - projectile.position;
        // Vector3 fireDirection = projectile.position - slingshotAnchor.position;

        // Reverse the direction to fire in
        fireDirection = -fireDirection;

        // Draw a line in the direction of the fired projectile
        // Debug.DrawRay(projectile.position, fireDirection * 10f, Color.green, 2f);

        // Apply the force to the projectile's rigidbody
        projectile.GetComponent<Rigidbody>().AddForce(fireDirection * fireSpeed, ForceMode.Impulse);

        projectile = null;
    }
}
