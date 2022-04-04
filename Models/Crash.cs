using System;
using System.ComponentModel.DataAnnotations;

namespace CrashySmashy.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }

        public int FIELD1 { get; set; }
        public DateTime CRASH_DATETIME { get; set; }
        public string ROUTE { get; set; }
        public decimal MILEPOINT { get; set; }
        public decimal LAT_UTM_Y { get; set; }
        public decimal LONG_UTM_X { get; set; }
        public string MAIN_ROAD_NAME { get; set; }
        public string CITY { get; set; }
        public string COUNTY_NAME { get; set; }
        public int CRASH_SEVERITY_ID { get; set; }
        public string WORKZONE_RELATED { get; set; }
        public string PEDESTRIAN_INVOLVED { get; set; }
        public string BICYCLIST_INVOLVED { get; set; }
        public string MOTORCYCLE_INVOLVED { get; set; }
        public string IMPROPER_RESTRAINT { get; set; }
        public string UNRESTRAINED { get; set; }
        public string DUI { get; set; }
        public string INTERSECTION_RELATED { get; set; }
        public string WILD_ANIMAL_RELATED { get; set; }
        public string DOMESTIC_ANIMAL_RELATED { get; set; }
        public string OVERTURN_ROLLOVER { get; set; }
        public string COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public string TEENAGE_DRIVER_INVOLVED { get; set; }
        public string OLDER_DRIVER_INVOLVED { get; set; }
        public string NIGHT_DARK_CONDITION { get; set; }
        public string SINGLE_VEHICLE { get; set; }
        public string DISTRACTED_DRIVING { get; set; }
        public string DROWSY_DRIVING { get; set; }
        public string ROADWAY_DEPARTURE { get; set; }
        //public bool WORKZONE_RELATED { get; set; }
        //public bool PEDESTRIAN_INVOLVED { get; set; }
        //public bool BICYCLIST_INVOLVED { get; set; }
        //public bool MOTORCYCLE_INVOLVED { get; set; }
        //public bool IMPROPER_RESTRAINT { get; set; }
        //public bool UNRESTRAINED { get; set; }
        //public bool DUI { get; set; }
        //public bool INTERSECTION_RELATED { get; set; }
        //public bool WILD_ANIMAL_RELATED { get; set; }
        //public bool DOMESTIC_ANIMAL_RELATED { get; set; }
        //public bool OVERTURN_ROLLOVER { get; set; }
        //public bool COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        //public bool TEENAGE_DRIVER_INVOLVED { get; set; }
        //public bool OLDER_DRIVER_INVOLVED { get; set; }
        //public bool NIGHT_DARK_CONDITION { get; set; }
        //public bool SINGLE_VEHICLE { get; set; }
        //public bool DISTRACTED_DRIVING { get; set; }
        //public bool DROWSY_DRIVING { get; set; }
        //public bool ROADWAY_DEPARTURE { get; set; }
    }
}
