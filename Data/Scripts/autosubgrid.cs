using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.ObjectBuilders;

namespace autosubgrid
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_MotorStator), false, "SubgridBase")]
    public class StatorTop : MyGameLogicComponent
    {
        Sandbox.ModAPI.IMyMotorStator stator;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            if (MyAPIGateway.Session.IsServer)
            {
                stator = Entity as Sandbox.ModAPI.IMyMotorStator;
                NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
            }
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (stator != null && stator.CubeGrid.Physics != null)
            {
                NeedsUpdate = MyEntityUpdateEnum.EACH_100TH_FRAME;
            }
        }

        public override void UpdateAfterSimulation100()
        {
            if (stator != null && stator.CubeGrid.Physics != null && stator.Top == null)
            {

                //MyVisualScriptLogicProvider.SendChatMessage("An Invincible Subgrid(tm) has lost is head for some reason. But This script should have put it back. Just letting you know :^)");
                stator.ApplyAction("AddRotorTopPart");
            }
        }

        public override void Close()
        {

        }
    }
}
