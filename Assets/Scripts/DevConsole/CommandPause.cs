using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandPause : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandPause()
        {
            Name = "Pause";
            Command = "pause";
            Description = "freeze game";
            Help = "Sets the overall speed of the game to zero";

            AddCommandToConsole();
        }

        public override void RunCommand(float index)
        {
            Time.timeScale = 0f;
        }

        public static CommandPause CreateCommand()
        {
            return new CommandPause();
        }
    }
}
