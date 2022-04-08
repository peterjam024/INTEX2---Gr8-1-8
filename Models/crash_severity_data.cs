using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashySmashy.Models
{
    public class crash_severity_data
    {
        public float milepoint { get; set; } = 4;
        public float hour { get; set; } = 0;
        public float month { get; set; } = 1;
        public float weekday { get; set; } = 0;
        public float work_zone_related_True { get; set; } = 0;
        public float pedestrian_involved_True { get; set; } = 0;
        public float bicyclist_involved_True { get; set; } = 0;
        public float motorcycle_involved_True { get; set; } = 0;
        public float improper_restraint_True { get; set; } = 0;
        public float unrestrained_True { get; set; } = 0;
        public float dui_True { get; set; } = 0;
        public float intersection_related_True { get; set; } = 0;
        public float wild_animal_related_True { get; set; } = 0;
        public float domestic_animal_related_True { get; set; } = 0;
        public float overturn_rollover_True { get; set; } = 0;
        public float commercial_motor_veh_involved_True { get; set; } = 0;
        public float teenage_driver_involved_True { get; set; } = 0;
        public float older_driver_involved_True { get; set; } = 0;
        public float night_dark_condition_True { get; set; } = 0;
        public float single_vehicle_True { get; set; } = 0;
        public float distracted_driving_True { get; set; } = 0;
        public float drowsy_driving_True { get; set; } = 0;
        public float roadway_departure_True { get; set; } = 0;
        public float county_name_Other { get; set; } = 0;
        public float county_name_SALT_LAKE { get; set; } = 0;
        public float county_name_UTAH { get; set; } = 0;
        public float county_name_WEBER { get; set; } = 0;
        public float city_Other { get; set; } = 0;
        public float city_SALT_LAKE_CITY { get; set; } = 0;
        public float city_WEST_VALLEY_CITY { get; set; } = 0;
        public float route_89 { get; set; } = 0;
        public float route_Other { get; set; } = 0;

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


        public float AdjustPrediction(crash_severity_data data , float prediction2)
        {
            float pred = prediction2;

            if (data.dui_True == 1)
            {
                pred = (float)(pred * 1.25);
            }
            if (data.improper_restraint_True == 1)
            {
                pred = (float)(pred * 1.5);
            }

            return pred;
        }


    }
}
