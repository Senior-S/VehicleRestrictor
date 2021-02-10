using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using VehicleRestrictor.Model;

namespace VehicleRestrictor.Commands
{
    public class CommandRestrictVehicle : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "restrictvehicle";

        public string Help => "Restrict a vehicle";

        public string Syntax => "<vehicle id> <permission>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "ss.restrictvehicle" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer user = (UnturnedPlayer)caller;
            VehicleRestrictor main = VehicleRestrictor.Instance;

            if (command.Length != 2)
            {
                UnturnedChat.Say(user, "Error! Correct usage /restrictvehicle <vehicle id> <permission>", UnityEngine.Color.red, false);
                return;
            }

            if (!ushort.TryParse(command[0], out ushort vehicleId))
            {
                UnturnedChat.Say(user, main.Translate("wrong_id"), UnityEngine.Color.red, false);
            }
            else
            {
                Vehicle toAdd = new Vehicle
                {
                    ID = vehicleId,
                    Permission = command[1]
                };

                main.Configuration.Instance.RestrictedVehicles.Add(toAdd);
                UnturnedChat.Say(user, main.Translate("vehicle_restricted"), UnityEngine.Color.green, true);
            }
        }
    }
}