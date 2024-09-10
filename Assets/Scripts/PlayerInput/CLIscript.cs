using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CLIscript : MonoBehaviour
{
    [SerializeField]
    TMP_Text output;
    [SerializeField]
    TMP_InputField iField;

    Regex r = new Regex(" ");
    private void Start()
    {
        CommandInterpreter.Instance.SetCLI(this);
        iField.ActivateInputField();
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Submit"))
        {
            string noWhiteSpace = r.Replace(iField.text, "");
            
            if(noWhiteSpace != "")
                SendCommand(iField.text);

            iField.text = "";
            iField.ActivateInputField();
        }
    }
    public void SendCommand(string input)
    {
        ShowOutput(input);

        string[] splitInput = input.Split(' ');
        List<string> args = new List<string>();
        for(int i = 1; i < splitInput.Length; i++)
        {
            args.Add(splitInput[i].ToLower());
        }

        CommandInterpreter.Instance.InterpretCommand(splitInput[0].ToLower(), args.ToArray());

        
    }
    public void ShowOutput(string o)
    {
        output.text += "\n" + o;
    }
    public void Reselect()
    {
        iField.ActivateInputField();
    }
}
