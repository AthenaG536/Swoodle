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

    // public Transform 
    void OnCollisionEnter(Collision collisionInfo) {
        Debug.Log (collisionInfo.collider.name);

        if(collisionInfo.collider.tag == "Letter Slot"){
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
