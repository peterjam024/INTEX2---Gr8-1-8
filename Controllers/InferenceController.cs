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
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [HttpPost]
        public ActionResult Score(crash_severity_data data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() * 100000 };
            result.Dispose();

            ViewBag.Prediction = prediction;
            return RedirectToAction("AnalyticsMaster");
        }

        [HttpGet]
        public IActionResult AnalyticsMaster()
        {
            return View();
        }

        public IActionResult Predictionoutput()
        {
            return View();
        }

    }
}