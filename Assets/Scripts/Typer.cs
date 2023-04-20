using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public List<Button> keyboardCharacterButtons = new List<Button>();

    private string characterNames = "QWERTYUIOPASDFGHJKLZXCVBNM";

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
    }

    void SetupButtons()
    {
        // Starting from the top row, set the text of the keyboard-texts to the ones in the list
        for (int i = 0; i < keyboardCharacterButtons.Count; i++)
        {
            keyboardCharacterButtons[i].transform.GetChild(0).GetComponent<Text>().text = characterNames[i].ToString();
        }

        // Whenever we click a button, run the function ClickCharacter and output the character to the Console.
        foreach (var keyboardButton in keyboardCharacterButtons)
        {
            string letter = keyboardButton.transform.GetChild(0).GetComponent<Text>().text;
            keyboardButton.GetComponent<Button>().onClick.AddListener(() => ClickCharacter(letter));
        }
    }

    void ClickCharacter(string letter)
    {
        Debug.Log(letter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}