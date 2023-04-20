using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guesser : MonoBehaviour
{
    // Blank word
    public string wordOut = null;
    public string currentWord = "Adieu";
    public string remainingWord = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentWord();
    }

    void SetCurrentWord()
    {
        // TODO: Add getting the current word from a word bank instead of a preset word
        wordOut = currentWord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CheckInput()
    {
        
    }

    void IsCorrectLetter(string letter)
    {
        // Update objects based on; correct letter in correct place, and correct letter but in the wrong place
    }

    void IsWordComplete()
    {
        // If word is complete show win message
    }
}
