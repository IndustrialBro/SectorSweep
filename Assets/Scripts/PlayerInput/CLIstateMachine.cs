using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CLIstateMachine : MonoBehaviour
{
    public static CLIstateMachine Instance {  get; private set; }
    private CLIstateMachine() { }

    [SerializeField]
    TMP_Text output, hintText;
    [SerializeField]
    TMP_InputField iField;

    CLIstate currState;
    CommandState cs = new CommandState();
    QueryState qs = new QueryState();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        hintText.text = "";
        iField.ActivateInputField();
        currState = cs;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            string noWhiteSpace = iField.text.Trim();

            if(noWhiteSpace != string.Empty)
            {
                currState.OnSubmit(iField.text);

                ShowOutput(iField.text);
                iField.text = "";
                iField.ActivateInputField();
            }
        }
        // špagety s nápovìdovou omajdou
        if(Input.GetKeyDown(KeyCode.Tab) && !string.IsNullOrEmpty(hintText.text))
        {
            iField.text = hintText.text;
            iField.caretPosition = iField.text.Length;
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
            hintText.text = "";

        if (Input.anyKeyDown && !string.IsNullOrEmpty(Input.inputString))
        {
            UpdateHintText();
        }
    }
    public void ShowOutput(string o)
    {
        output.text += "\n" + o;
    }
    public void EnterQueryState(string desc, params QOption[] options)
    {
        currState = qs;
        qs.NewQuery(desc, options);
    }
    public void EnterCommandState()
    {
        currState = cs;
    }
    void UpdateHintText()
    {
        if (!string.IsNullOrEmpty(iField.text))
        {
            string[] splitInput = iField.text.Split(' ');
            Regex r = new Regex("u[1-9]");

            int i = 0;
            for (int j = 0; j < splitInput.Length; j++)
                if (r.IsMatch(splitInput[j])) i++;

            List<string> strings = new List<string>();
            for (; i < splitInput.Length; i++) strings.Add(splitInput[i]);

            string hint = CommandInterpreter.Instance.UpdateHint(strings.ToArray());
            if (!string.IsNullOrEmpty(hint))
            {
                char[] hintTextArray = new char[hint.Length - splitInput[splitInput.Length - 1].Length];
                for (int j = 0; j < hintTextArray.Length; j++) hintTextArray[j] = hint[j + (hint.Length - hintTextArray.Length)];

                hintText.text = iField.text + new string(hintTextArray);
            }
            else hintText.text = "";
        }
        else hintText.text = "";
    }
}
public abstract class CLIstate
{
    public abstract void OnSubmit(string input);
}
public class CommandState : CLIstate
{

    Regex r = new Regex("u[1-9]");
    public override void OnSubmit(string input)
    {
        SendCommand(input);
    }

    public void SendCommand(string input)
    {
        // jednotky pøíkaz argumenty

        string[] splitInput = input.Split(' ');

        for (int i = 0; i < splitInput.Length; i++) splitInput[i] = splitInput[i].ToLower();

        List<string> units = new List<string>();
        if (splitInput[0] == "all")
        { //Všechny jednotky budou trsat
            CommandInterpreter.Instance.InterpretCommand(splitInput[1], GetArgs(splitInput, 2), ComListThingamabob.Instance.GetListeners(new string[] { "all" }));
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
            CommandInterpreter.Instance.InterpretCommand(splitInput[0], GetArgs(splitInput, 1));
        }
    }
    string[] GetArgs(string[] a, int startIndex)
    {
        List<string> args = new List<string>();
        for (int i = startIndex; i < a.Length; i++)
        {
            args.Add(a[i]);
        }

        return args.ToArray();
    }
}
public class QueryState : CLIstate
{
    QOption[] options;
    string output;
    public void NewQuery(string desc, QOption[] options)
    {
        output = "";
        this.options = options;

        output += desc;
        for(int i = 0; i < options.Length; i++)
        {
            output += $"\n [{i}] {options[i].desc}";
        }

        CLIstateMachine.Instance.ShowOutput(output);
    }
    public override void OnSubmit(string input)
    {
        if(int.TryParse(input, out int res) && res < options.Length)
        {
            options[res].DoStuff();
            CLIstateMachine.Instance.EnterCommandState();
        }else 
        {
            CLIstateMachine.Instance.ShowOutput("Please input a number within reason");
        }
    }
}
public abstract class QOption
{
    public string desc { get; protected set; }
    public QOption(string desc)
    {
        this.desc = desc;
    }

    public abstract void DoStuff();
}
public class HAUOption : QOption
{
    // Heal All Units Option - vyléèí všechny hráèovo jednotky (pøekvapivì)
    public HAUOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        Reward.HAU?.Invoke();
        ObjectiveManager.Instance.NextObjective();
    }
}
public class IDOption : QOption
{
    public IDOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        Reward.ID?.Invoke();
        ObjectiveManager.Instance.NextObjective();
    }
}
public class IMHPOption : QOption
{
    public IMHPOption(string desc) : base(desc)
    {
    }

    public override void DoStuff()
    {
        Reward.IMHP?.Invoke();
        ObjectiveManager.Instance.NextObjective();
    }
}