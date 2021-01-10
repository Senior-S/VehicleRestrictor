using Rocket.API;
using System.Collections.Generic;
using VehicleRestrictor.Model;

namespace VehicleRestrictor
{
    public class Configuration : IRocketPluginConfiguration
    {
        public void LoadDefaults()
        {
            Enabled = true;
            RestrictedVehicles = new List<Vehicle> { toAdd };
        }

        public bool Enabled;

        Vehicle toAdd = new Vehicle
        {
            ID = 1,
            Permission = "ss.blackoffroader"
        };

        public List<Vehicle> RestrictedVehicles;
    }
}
