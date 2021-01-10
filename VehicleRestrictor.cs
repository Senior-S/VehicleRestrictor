using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;
using Logger = Rocket.Core.Logging.Logger;

namespace VehicleRestrictor
{
    public class VehicleRestrictor : RocketPlugin<Configuration>
    {
        internal static VehicleRestrictor Instance;

        protected override void Load()
        {
            Logger.Log(" Plugin loaded correctly!");
            Logger.Log(" More plugins: www.dvtserver.xyz");
            if (!Configuration.Instance.Enabled)
            {
                Logger.Log(" Plugin disabled! Please enable it in the config.");
                this.Unload();
                return;
            }

            Instance = this;

            VehicleManager.onEnterVehicleRequested += OnEnterVehicleRequested;
        }

        private void OnEnterVehicleRequested(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            var check = Configuration.Instance.RestrictedVehicles.Where(v => v.ID == vehicle.id).FirstOrDefault();
            if (check != null)
            {
                UnturnedPlayer user = UnturnedPlayer.FromPlayer(player);
                if (!R.Permissions.HasPermission(user, new List<string> { check.Permission }))
                {
                    shouldAllow = false;
                    UnturnedChat.Say(user, Translate("no_permission"), true);
                }
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    { "no_permission", "You don't have permission to enter in this vehicle." },
                    { "wrong_id", "Error! Specify a valid id!" },
                    { "vehicle_restricted", "Vehicle restricted successfully" },
                    { "error_no_idvehicle", "The vehicle with id {0} are not restricted." },
                    { "vehicle_unrestricted", "The vehicle with id {0} are now unrestricted." }
                };
            }
        }

        protected override void Unload()
        {
            VehicleManager.onEnterVehicleRequested -= OnEnterVehicleRequested;
            Logger.Log(" Plugin unloaded correctly!");
        }
    }
}