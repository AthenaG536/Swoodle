using UnityEngine;
using TMPro;

public class letterCollision : MonoBehaviour
{
    public TMP_Text letterText;
    public TMP_Text slotText;

    // public Transform 
    void OnCollisionEnter(Collision collisionInfo) {
        Debug.Log (collisionInfo.collider.name);

        if(collisionInfo.collider.tag == "Letter Slot"){
            Debug.Log("Letter hit a letter slot");
            Debug.Log("The Letter was...");
            Debug.Log(letterText.text);
            slotText.text = letterText.text;
        }

    }
}
