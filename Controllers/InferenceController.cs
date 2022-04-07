using System;
using System.Collections.Generic;
using System.Linq;
using CrashySmashy.Models;
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
        public ActionResult Score(crash_severity_data data)
        {
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