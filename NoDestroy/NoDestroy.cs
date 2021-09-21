using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Linq;
using UnityEngine;

namespace NoDestroy
{
    public class NoDestroy : RocketPlugin<Config>
    {
        public override void LoadPlugin()
        {
            base.LoadPlugin();

            if (Configuration.Instance.Enabled)
            {
                StructureManager.onDamageStructureRequested += OnDamageStructureRequested;
                BarricadeManager.onDamageBarricadeRequested += OnDamageStructureRequested;
                VehicleManager.onDamageVehicleRequested += OnDamageVehicleRequested;
            }
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            base.UnloadPlugin(state);

            StructureManager.onDamageStructureRequested -= OnDamageStructureRequested;
            BarricadeManager.onDamageBarricadeRequested -= OnDamageStructureRequested;
            VehicleManager.onDamageVehicleRequested -= OnDamageVehicleRequested;
        }

        public EDamageOrigin[] AllowSources = new EDamageOrigin[] { EDamageOrigin.Carepackage_Timeout, EDamageOrigin.Horde_Beacon_Self_Destruct, EDamageOrigin.Kill_Volume, EDamageOrigin.Plant_Harvested, EDamageOrigin.Trap_Wear_And_Tear, EDamageOrigin.Unknown, EDamageOrigin.Vehicle_Collision_Self_Damage };
        public EDamageOrigin[] VehicleAllowSources = new EDamageOrigin[] { EDamageOrigin.Zombie_Electric_Shock, EDamageOrigin.Zombie_Fire_Breath, EDamageOrigin.Zombie_Stomp, EDamageOrigin.Zombie_Swipe, EDamageOrigin.Flamable_Zombie_Explosion, EDamageOrigin.Mega_Zombie_Boulder, EDamageOrigin.Radioactive_Zombie_Explosion, EDamageOrigin.Animal_Attack, EDamageOrigin.Kill_Volume, EDamageOrigin.Unknown, EDamageOrigin.Vehicle_Bumper, EDamageOrigin.Vehicle_Collision_Self_Damage, EDamageOrigin.Vehicle_Explosion };

        public void OnDamageStructureRequested(CSteamID instigatorSteamID, Transform structureTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            if (!AllowSources.Contains(damageOrigin))
            {
                var pl = PlayerTool.getPlayer(instigatorSteamID);
                if (pl != null)
                {
                    var upl = UnturnedPlayer.FromPlayer(pl);
                    shouldAllow = upl.GetPermissions().Any(x => x.Name.Equals("NoDestroy.Bypass", System.StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    shouldAllow = false;
                }
            }
        }

        public void OnDamageVehicleRequested(CSteamID instigatorSteamID, InteractableVehicle vehicle, ref ushort pendingTotalDamage, ref bool canRepair, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            if (!VehicleAllowSources.Contains(damageOrigin))
            {
                var pl = PlayerTool.getPlayer(instigatorSteamID);
                if (pl != null)
                {
                    var upl = UnturnedPlayer.FromPlayer(pl);
                    shouldAllow = upl.GetPermissions().Any(x => x.Name.Equals("NoDestroy.Bypass", System.StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    shouldAllow = false;
                }
            }
        }
    }
}