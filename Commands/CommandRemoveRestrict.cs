using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using System.Linq;

namespace VehicleRestrictor.Commands
{
    public class CommandRemoveRestrict : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "removerestrict";

        public string Help => "Remove the restrict from a vehicle";

        public string Syntax => "<vehicle id>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "ss.removerestrict" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer user = (UnturnedPlayer)caller;
            VehicleRestrictor main = VehicleRestrictor.Instance;

            if (command.Length != 2)
            {
                UnturnedChat.Say(user, "Error! Correct usage /removerestrict <vehicle id>", UnityEngine.Color.red, false);
                return;
            }

            if (!ushort.TryParse(command[0], out ushort vehicleId))
            {
                UnturnedChat.Say(user, main.Translate("wrong_id"), UnityEngine.Color.red, false);
            }
            else
            {
                var z = main.Configuration.Instance.RestrictedVehicles.Where(v => v.ID == vehicleId).FirstOrDefault();
                if (z == null)
                {
                    UnturnedChat.Say(user, main.Translate("error_no_idvehicle", vehicleId.ToString()), UnityEngine.Color.green, true);
                }
                else
                {
                    main.Configuration.Instance.RestrictedVehicles.Remove(z);
                    UnturnedChat.Say(user, main.Translate("vehicle_unrestricted", vehicleId.ToString()), UnityEngine.Color.green, true);
                }
            }
        }
    }
}