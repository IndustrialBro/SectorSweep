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

    Regex r = new Regex("u[1-9]");

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
            string noWhiteSpace = iField.text.Trim();
            
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
        
        for(int i = 0; i < splitInput.Length; i++) splitInput[i] = splitInput[i].ToLower();

        List<string> units = new List<string>();
        if (splitInput[0] == "all")
        { //Všechny jednotky budou trsat
            CommandInterpreter.Instance.InterpretCommand(splitInput[1], GetArgs(splitInput, 2), ComListThingamabob.Instance.GetListeners(new string[]{ "all" } ));
        }
        else if (r.IsMatch(splitInput[0]))
        { //Jen specifikované jednotky budou trsat
            int i;
            for (i = 0; i < splitInput.Length; i++)
            {
                if (r.IsMatch(splitInput[i])) units.Add(splitInput[i]);
                else break;
            }
            Debug.Log($"Jednotky: {units.Count}");

            CommandInterpreter.Instance.InterpretCommand(splitInput[i], GetArgs(splitInput, i + 1), ComListThingamabob.Instance.GetListeners(units.ToArray()));
        } 
        else
        { //Pøíkaz není o trsání

        }
    }
    string[] GetArgs(string[] a, int startIndex)
    {
        List<string> args = new List<string>();
        for(int i = startIndex; i < a.Length; i++)
        {
            args.Add(a[i]);
        }

        return args.ToArray();
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
