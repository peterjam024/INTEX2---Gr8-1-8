using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrashySmashy.Models
{
    public class crash_severity_data
    {
        public float MILEPOINT { get; set; }
        public float LAT_UTM_Y { get; set; }
        public float LONG_UTM_X { get; set; }
        public float COUNTY_NAME_SALT_Lake { get; set; }
        public float INTERSECTION_RELATED_TRUE { get; set; }
        public float TEENAGE_DRIVER_INVOLVED_TRUE { get; set; }
        public float NIGHT_DARK_CONDITION_TRUE { get; set; }
        public float SINGLE_VEHICLE_TRUE { get; set; }
        public float HOUR { get; set; }
        public float MONTH { get; set; }
        public float WEEKDAY { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            MILEPOINT, LAT_UTM_Y, LONG_UTM_X, COUNTY_NAME_SALT_Lake,
            INTERSECTION_RELATED_TRUE, TEENAGE_DRIVER_INVOLVED_TRUE, NIGHT_DARK_CONDITION_TRUE, SINGLE_VEHICLE_TRUE, HOUR, MONTH, WEEKDAY
            };
            int[] dimensions = new int[] { 1, 11 };
            return new DenseTensor<float>(data, dimensions);
        }

    }
}
