using System;
using System.Collections.Generic;
using System.Linq;
using CrashySmashy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;


namespace aspnetcore.Controllers
{
    public class InferenceController : Controller
    {

        //Create a new session of type inferencesession
        private InferenceSession _session;


        //initialize the session, and assign it to the value of session that we passed in.
        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        //
        [HttpPost]
        public ActionResult Score(crash_severity_data data, IFormCollection form)
        {

            //          MAKE OTHER .CS MODELS TO GIVE YOU OTHER CLASSES TO USE

            decimal city = Convert.ToDecimal(form["City"]);
            decimal county = Convert.ToDecimal(form["County"]);



            if (city == 1)
                data.city_SALT_LAKE_CITY = 1;
            else if (city == 2)
                data.city_WEST_VALLEY_CITY = 1;
            else
                data.city_Other = 1;



            if (county == 1)
                data.county_name_SALT_LAKE = 1;
            else if (county == 2)
                data.county_name_WEBER = 1;
            else if (county == 3)
                data.county_name_UTAH = 1;
            else
                data.county_name_Other = 1;




            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();

            return RedirectToAction("Predictionoutput", prediction);
        }

        [HttpGet]
        public IActionResult AnalyticsMaster()
        {
            return View();
        }

        public IActionResult Predictionoutput(Prediction prediction)
        {
            var prediction2 = prediction.PredictedValue;
            ViewBag.prediction = Math.Floor(prediction2);

            return View();
        }

    }
}