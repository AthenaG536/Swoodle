using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTyper : MonoBehaviour
{
    public Camera mainCameraObj;
    public Button rotateButton;
    public float rotationSpeed = 10f;
    public float buttonRotationDuration = 0.3f;
    // private bool isRotating = false;
    private bool isAtZeroRotation = true; 


    // private bool isAtTyper = false;
    private Quaternion buttonRotationAtZero;
    private Quaternion buttonRotationAtTyper;

    private void Start()
    {
        buttonRotationAtZero = rotateButton.transform.rotation;
        buttonRotationAtTyper = Quaternion.Euler(buttonRotationAtZero.eulerAngles.x, buttonRotationAtZero.eulerAngles.y, 180f);
    }

    public void DownToTyper()
    {

        if(mainCameraObj){
            Quaternion targetRotation = Quaternion.Euler(14.6f, mainCameraObj.transform.rotation.eulerAngles.y, mainCameraObj.transform.rotation.eulerAngles.z);
            if(Mathf.Approximately(mainCameraObj.transform.rotation.eulerAngles.x, 0f))
            {
                StartCoroutine(RotateCamera(mainCameraObj.transform.rotation, targetRotation, rotationSpeed));
                StartCoroutine(RotateButton(rotateButton.transform.rotation, buttonRotationAtTyper, buttonRotationDuration));
            }
            else if(Mathf.Approximately(mainCameraObj.transform.rotation.eulerAngles.x, 14.6f))
            {
                StartCoroutine(RotateCamera(mainCameraObj.transform.rotation, Quaternion.Euler(0f, mainCameraObj.transform.rotation.eulerAngles.y, mainCameraObj.transform.rotation.eulerAngles.z), rotationSpeed));
                StartCoroutine(RotateButton(rotateButton.transform.rotation, buttonRotationAtZero, buttonRotationDuration));
            }
        }
    }

    private IEnumerator RotateCamera(Quaternion startRotation, Quaternion targetRotation, float rotationSpeed = 5.0f)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            mainCameraObj.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // isRotating = false; // Set the flag to false to indicate that the rotation is complete
        isAtZeroRotation = !isAtZeroRotation; // Toggle the isAtZeroRotation flag
    }
    private IEnumerator RotateButton(Quaternion startRotation, Quaternion targetRotation, float duration = 5.0f)
    {   
        float time = 0f;
        while (time < duration)
        {
            rotateButton.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rotateButton.transform.rotation = targetRotation;
    }
}
