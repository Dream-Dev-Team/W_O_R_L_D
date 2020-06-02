using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandTeleportPlayer : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandTeleportPlayer()
        {
            Name = "TeleportPlayer";
            Command = "teleportPlayer";
            Description = "teleports player";
            Help = "teleports player to: arg1 = xPos, arg2 = yPos";

            AddCommandToConsole();
        }

        public override void RunCommand(float index, float index2)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(index, index2, 0f);
            Debug.Log("teleported player to: " + "[" + index + ", " + index2 + "]");
        }


        public static CommandTeleportPlayer CreateCommand()
        {
            return new CommandTeleportPlayer();
        }
    }
}
