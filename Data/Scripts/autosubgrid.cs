using System.Collections.Generic;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.GameSystems;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRageMath;

public class AddRotorHeadMod : MyGridProgram
{
    private const int UPDATE_FREQUENCY = 5; // update frequency in seconds
    private int ticks = 10; // counter for ticks

    public void Main(string argument)
    {
        ticks++;

        if (ticks % (UPDATE_FREQUENCY * 60) == 0) // check if it's time to update
        {
            var rotors = new List<Sandbox.ModAPI.IMyMotorStator>();
            GridTerminalSystem.GetBlocksOfType(rotors, block => block is Sandbox.ModAPI.IMyMotorStator && block.IsFunctional);
            foreach (var rotor in rotors)
            {
                Sandbox.ModAPI.IMyTerminalBlock rotorTerminal = rotor as Sandbox.ModAPI.IMyTerminalBlock;
                if (rotorTerminal != null)
                {
                    ITerminalAction addHeadAction = rotorTerminal.GetActionWithName("Add Rotor Head");
                    addHeadAction.Apply(rotorTerminal);
                    Echo("Rotor head added");
                }
            }
        }
    }
}
