using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashySmashy.Models
{
    public class crash_severity_data
    {
        public float milepoint { get; set; }
        public float hour { get; set; }
        public float month { get; set; }
        public float weekday { get; set; }
        public float work_zone_related_True { get; set; }
        public float pedestrian_involved_True { get; set; }
        public float bicyclist_involved_True { get; set; }
        public float motorcycle_involved_True { get; set; }
        public float improper_restraint_True { get; set; }
        public float unrestrained_True { get; set; }
        public float dui_True { get; set; }
        public float intersection_related_True { get; set; }
        public float wild_animal_related_True { get; set; }
        public float domestic_animal_related_True { get; set; }
        public float overturn_rollover_True { get; set; }
        public float commercial_motor_veh_involved_True { get; set; }
        public float teenage_driver_involved_True { get; set; }
        public float older_driver_involved_True { get; set; }
        public float night_dark_condition_True { get; set; }
        public float single_vehicle_True { get; set; }
        public float distracted_driving_True { get; set; }
        public float drowsy_driving_True { get; set; }
        public float roadway_departure_True { get; set; }
        public float county_name_Other { get; set; }
        public float county_name_SALT_LAKE { get; set; }
        public float county_name_UTAH { get; set; }
        public float county_name_WEBER { get; set; }
        public float city_Other { get; set; }
        public float city_SALT_LAKE_CITY { get; set; }
        public float city_WEST_VALLEY_CITY { get; set; }
        public float route_89 { get; set; }
        public float route_Other { get; set; }

    public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            milepoint, hour, month, weekday, work_zone_related_True, pedestrian_involved_True, bicyclist_involved_True, motorcycle_involved_True, 
            improper_restraint_True, unrestrained_True, dui_True, intersection_related_True, wild_animal_related_True, domestic_animal_related_True,
            overturn_rollover_True, commercial_motor_veh_involved_True, teenage_driver_involved_True, older_driver_involved_True, night_dark_condition_True,
            single_vehicle_True, distracted_driving_True, drowsy_driving_True, roadway_departure_True, county_name_Other, county_name_SALT_LAKE, county_name_UTAH,
            county_name_WEBER, city_Other, city_SALT_LAKE_CITY, city_WEST_VALLEY_CITY, route_89, route_Other
            };
            int[] dimensions = new int[] { 1, 32 };
            return new DenseTensor<float>(data, dimensions);
        }

    }
}
