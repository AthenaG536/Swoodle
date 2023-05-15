using UnityEngine;
using TMPro;

public class letterCollision : MonoBehaviour
{
    public TMP_Text letterText;
    public TMP_Text slotText;
    public TMP_Text slot1;
    public TMP_Text slot2;
    public TMP_Text slot3;
    public TMP_Text slot4;
    public TMP_Text slot5;
    public SpringJoint springJoint;
    public AudioSource groundCollideSound;
    public AudioSource letterCollideSound;

    public Rigidbody projectile;
    Vector3 startingPosition = new Vector3(0.25f, 1.12f, 31f);
    Vector3 startingRotation = new Vector3(0f, 0f, 0f);


    // public Transform 
    void OnCollisionEnter(Collision collisionInfo) {
      if(collisionInfo.collider.name == "Ground"){
        if(projectile && (projectile.position.z > 32 || projectile.position.z < 30)){
          groundCollideSound.Play();  // Sound effect
        }
        if(projectile && projectile.position.z > 32){
          // resets the spring joint for the slingshot
          if(springJoint) springJoint.breakForce = Mathf.Infinity;
          Debug.Log("letter block flung");

          // Reset the position of the projectile
          projectile.velocity = Vector3.zero;
          projectile.angularVelocity = Vector3.zero;
          projectile.position = startingPosition;
          projectile.rotation = Quaternion.identity;

          // springJoint.connectedAnchor = startingPosition;
        }


      }

      if(collisionInfo.collider.tag == "Letter Slot"){
          letterCollideSound.Play(); // Sound effect

          Debug.Log (collisionInfo.collider.name);
          Debug.Log("Letter hit a letter slot");
          if(slotText && letterText){
              slotText.text = letterText.text;
          }

          if(letterText){
              string slotNumber = collisionInfo.collider.name.Substring(collisionInfo.collider.name.Length - 1);
              switch(slotNumber) 
              {
                case "1":
                  slot1.text = letterText.text;
                  break;
                case "2":
                  slot2.text = letterText.text;
                  break;
                case "3":
                  slot3.text = letterText.text;
                  break;
                case "4":
                  slot4.text = letterText.text;
                  break;
                case "5":
                  slot5.text = letterText.text;
                  break;
                default:
                  break;
              }


          }
          
      }

    }
}
