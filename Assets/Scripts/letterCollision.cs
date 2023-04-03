using UnityEngine;

public class letterCollision : MonoBehaviour
{
    // public Transform 
    void OnCollisionEnter(Collision collisionInfo) {
        Debug.Log (collisionInfo.collider.name);

        if(collisionInfo.collider.tag == "Letter Slot"){
            Debug.Log("Letter hit a letter slot");
        }

    }
}
