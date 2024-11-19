using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public sealed class CLIscript : MonoBehaviour
{
    public static CLIscript Instance { get; private set; } = null;
    private CLIscript() {}

    [SerializeField]
    TMP_Text output;
    [SerializeField]
    TMP_InputField iField;

    Regex r = new Regex(" ");

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        iField.ActivateInputField();
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Submit"))
        {
            string noWhiteSpace = r.Replace(iField.text, "");
            
            if(noWhiteSpace != string.Empty)
                SendCommand(iField.text);

            iField.text = "";
            iField.ActivateInputField();
        }
    }
    public void SendCommand(string input)
    {
        // jednotky pøíkaz argumenty
        ShowOutput(input);

        string[] splitInput = input.Split(' ');
        string[] units = splitInput[0].Split(',');
        List<string> args = new List<string>();
        for(int i = 2; i < splitInput.Length; i++)
        {
            args.Add(splitInput[i].ToLower());
        }

        CommandInterpreter.Instance.InterpretCommand(splitInput[1].ToLower(), args.ToArray(), ComListThingamabob.Instance.GetListeners(units));
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
