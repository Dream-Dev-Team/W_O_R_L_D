using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandKill : ConsoleCommand

    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandKill()
        {
            Name = "Kill";
            Command = "kill";
            Description = "kills player";
            Help = "Use this command to kill Player when stuck";

            AddCommandToConsole();
        }

        public override void RunCommand(float index, float index2)
        {

        }


        public static CommandKill CreateCommand()
        {
            return new CommandKill();
        }
    }
}