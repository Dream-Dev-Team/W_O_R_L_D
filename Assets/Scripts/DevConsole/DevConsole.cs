using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            string addMessage = " command added to console";

            DevConsole.AddCommandsToConsole(Command, this);
            Debug.Log(Name + addMessage);
        }

        public abstract void RunCommand(float index);
    }
    public class DevConsole : MonoBehaviour
    {
        public static DevConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }


        [Header("UI Components")]
        public Canvas consoleCanvas;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;

        public GameObject fastInputElement;
        public Transform layoutInputElements;
        public int currentCharsInInput = 0; //Um 1 Verzögert
        private bool activefastIElements = false;

        public int allowedNumberOfSentences = 100;
        private int numberOfSentences = 0;

        [Header("Noise Elements")]
        public PlayerController playerController;
        public WindowController windowController;

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            CreateCommands();
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logMessage, string stackTrace, LogType type)       //Debug.Log and Error Messages to console
        {
            string _message = "";
            switch(type)
            {
                case LogType.Log:
                    _message = "[<color=cyan>" + type.ToString() + "</color>] " + logMessage; break;
                case LogType.Warning:
                    _message = "[<color=yellow>" + type.ToString() + "</color>] " + logMessage; break;
                case LogType.Error:
                    _message = "[<color=red>" + type.ToString() + "</color>] " + logMessage; break;
                case LogType.Exception:
                    _message = "[<color=magenta>" + type.ToString() + "</color>] " + logMessage; break;
                default:
                    _message = "[<color=white>" + type.ToString() + "</color>] " + logMessage; break;
            }
            AddMessageToConsole(_message);
        }

        private void CreateCommands()
        {
            CommandClear.CreateCommand();
            CommandQuit.CreateCommand();
            CommandPause.CreateCommand();
            CommandTimeScale.CreateCommand();
            //CommandKill.CreateCommand();
            CommandShowGameDataPath.CreateCommand();
        }

        public static void AddCommandsToConsole(string _name, ConsoleCommand _command)
        {
            if (!Commands.ContainsKey(_name))
            {
                Commands.Add(_name, _command);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);

                if(consoleCanvas.isActiveAndEnabled)
                {
                    //Cursor.lockState = CursorLockMode.None;
                    //Cursor.visible = true;

                    consoleInput.text = "";
                    consoleInput.Select();
                    consoleInput.ActivateInputField();

                    DisableNoiseInputs();
                }
                else
                {
                   //Cursor.lockState = CursorLockMode.Locked;
                   //Cursor.visible = false;

                    EnableNoiseInputs();
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                AddFirstCommandToInputField();
            }


            if (consoleCanvas.isActiveAndEnabled)
            {
                if(inputText.text.Length == 0 && activefastIElements)
                {
                    DeleteAllFastInputBtns();
                }
                if (inputText.text != "" && inputText.text.Length > currentCharsInInput || inputText.text.Length < currentCharsInInput && inputText.text.Length != 0 || inputText.text.Length == 1 && currentCharsInInput == 0) //last two for first inputed Char (!currentchar will be == textLength in second input, when all chars were deleted from TextfieldText [currentChars always behind actual count])
                {
                    currentCharsInInput = inputText.text.Length;
                    SearchCommand(inputText.text);
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        ParseInput(inputText.text);
                        consoleInput.text = "";
                        consoleInput.Select();
                        consoleInput.ActivateInputField();
                    }
                }
            }
        }

        private void DisableNoiseInputs()
        {
            playerController.enabled = false;
            windowController.enabled = false;
        }
        private void EnableNoiseInputs()
        {
            playerController.enabled = true;
            windowController.enabled = true;
        }




        public void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
            numberOfSentences++;
            if (numberOfSentences >= allowedNumberOfSentences)
            {
                DeleteOldText();
                numberOfSentences = 0;
            }
        }


        private void ParseInput(string input)
        {
            string[] _input = input.Split(null);
            float index = 0f;

            if (_input.Length == 0 || _input == null)
            {
                Debug.LogWarning("Command not recognized");
                return;
            }

            if (!Commands.ContainsKey(_input[0]))
            {
                Debug.LogWarning("Command not found");
            }
            else
            {
                if (_input.Length >= 2)
                {
                    index = float.Parse(_input[1]);
                }
                Commands[_input[0]].RunCommand(index);
            }
        }


        private void DeleteOldText()
        {
            consoleText.text = "";
        }

        private void SearchCommand(string searchInput)
        {
            foreach(KeyValuePair<string, ConsoleCommand> entry in Commands)
            {
                if(entry.Value.Command.Contains(searchInput))
                {
                    activefastIElements = true;
                    AddFastInputBtn(entry.Value.Command);
                }
                else
                {
                    DeleteFastInputBtn(entry.Value.Command);
                }
            }
        }

        private void AddFastInputBtn(string command)
        {
            if(layoutInputElements.transform.childCount > 0)
            {
                for (int i = 0; i < layoutInputElements.childCount; i++)
                {
                    if (layoutInputElements.transform.GetChild(i).transform.name.CompareTo(command) == 0)
                    {
                        GameObject element = layoutInputElements.GetChild(i).gameObject;
                        Destroy(element);
                    }
                }
            }

            GameObject clone = Instantiate(fastInputElement);
            clone.transform.SetParent(layoutInputElements, false);
            clone.transform.name = command;
            clone.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { AddInputFunction(command); });
            clone.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = command;
        }
        private void DeleteFastInputBtn(string command)
        {
            for(int i = 0; i < layoutInputElements.childCount; i++)
            {
                if(layoutInputElements.transform.GetChild(i).transform.name.CompareTo(command) == 0)
                {
                    GameObject element = layoutInputElements.GetChild(i).gameObject;
                    Destroy(element);
                }
            }
        }
        private void  DeleteAllFastInputBtns()
        {
            for (int i = 0; i < layoutInputElements.childCount; i++)
            {
                GameObject element = layoutInputElements.GetChild(i).gameObject;
                Destroy(element);
            }
            activefastIElements = false;
        }

        public void AddInputFunction(string command)
        {
            consoleInput.text = command;
            StartCoroutine(DebugSelection());
            consoleInput.Select();
        }
        private IEnumerator DebugSelection()
        {
            yield return new WaitForEndOfFrame();
            consoleInput.MoveTextEnd(true);
        }

        private void AddFirstCommandToInputField()
        {
            if(layoutInputElements.childCount > 0)
            {
                AddInputFunction(layoutInputElements.GetChild(0).transform.name);
            }
        }
    }

}
