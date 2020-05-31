using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandTimeScale : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandTimeScale()
        {
            Name = "TimeScale";
            Command = "timeScale";
            Description = "Sets the timeScale";
            Help = "Sets the overall speed of the game to the given value (float)";

            AddCommandToConsole();
        }

        public override void RunCommand(float index)
        {
            Time.timeScale = index;
        }

        public static CommandTimeScale CreateCommand()
        {
            return new CommandTimeScale();
        }
    }
}
