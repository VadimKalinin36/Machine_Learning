using Machine_Learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Machine_Learning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PredictionEnginePool<MLImageModel.ModelInput, MLImageModel.ModelOutput> _predictionImage;

        public HomeController(ILogger<HomeController> logger,
            PredictionEnginePool<MLImageModel.ModelInput, MLImageModel.ModelOutput> predictionImage)
        {
            _logger = logger;
            _predictionImage = predictionImage;
        }

        //[HttpPost]
        //public IActionResult Index(IFormFile file)
        //{
        //    using MemoryStream ms = new MemoryStream();
        //    file.OpenReadStream().CopyTo(ms);
        //    var bytes = ms.ToArray();

        //    MLModel.ModelInput sampleImage = new MLModel.ModelInput()
        //    {
        //        ImageSource = bytes
        //    };

        //    var predictImage = _predictionImage.Predict(sampleImage);
        //    var labels = ModelLabel.GetLabels();

        //    for (int i = 0; i < predictImage.Score.Length; i++)
        //    {
        //        var label = labels.FirstOrDefault(x => x.Id == i);

        //        if (label != null) label.Score = predictImage.Score[i];
        //    }

        //    labels = labels.OrderByDescending(x => x.Score).Take(2).ToList();

        //    return Json(new { labels });
        //}

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            using MemoryStream ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);
            var bytes = ms.ToArray();

            MLImageModel.ModelInput sampleImage = new MLImageModel.ModelInput()
            {
                ImageSource = bytes
            };

            var predictImage = _predictionImage.Predict(sampleImage);
            var labels = ModelLabel.GetLabels();

            for (int i = 0; i < predictImage.Score.Length; i++)
            {
                var label = labels.FirstOrDefault(x => x.Id == i);

                if (label != null) label.Score = predictImage.Score[i];
            }

            labels = labels.OrderByDescending(x => x.Score).Take(2).ToList();

            return Json(new { labels });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
