using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ChangeLetter : MonoBehaviour
{
    public TMP_Text letterText;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    public void ChangeCurrentLetter(string letter)
    {
        letterText.text = letter;
    }

}
