using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandClear : ConsoleCommand

    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandClear()
        {
            Name = "Clear";
            Command = "clear";
            Description = "Clears console text";
            Help = "Use this command with 0 arguments for clearing the Console";

            AddCommandToConsole();
        }

        public override void RunCommand(float index)
        {
            DevConsole console = GameObject.FindGameObjectWithTag("DevConsole").GetComponent<DevConsole>();
            console.consoleText.text = "";
        }

        public static CommandClear CreateCommand()
        {
            return new CommandClear();
        }
    }
}