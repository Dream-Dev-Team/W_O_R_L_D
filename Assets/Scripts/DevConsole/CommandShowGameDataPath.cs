using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Console
{
    public class CommandShowGameDataPath : ConsoleCommand

    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandShowGameDataPath()
        {
            Name = "ShowGameDataPath";
            Command = "showGameDataPath";
            Description = "Shows the directory where all files are saved by the Game in the windows file explorer";
            Help = "Use this command with 0 argument to show the game-save-data in the windows file explorer";

            AddCommandToConsole();
        }

        public override void RunCommand(float index)
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath + "\\W_O_R_L_D");
        }

        public static CommandShowGameDataPath CreateCommand()
        {
            return new CommandShowGameDataPath();
        }
    }
}