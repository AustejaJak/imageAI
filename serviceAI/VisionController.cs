using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace serviceAI;

[ApiController]
[Route("api/[controller]")]
public class VisionController : ControllerBase
{
    private readonly VisionService _visionService;

    public VisionController(VisionService visionService)
    {
        _visionService = visionService;
    }

    [HttpGet("analyze")]
    public async Task<IActionResult> AnalyzeImage([FromQuery] string imageUrl)
    {
        var client = _visionService.Authenticate();

        var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Description };
        var result = await client.AnalyzeImageAsync(imageUrl, visualFeatures: features);

        var descriptions = result.Description.Captions.Select(c => new
        {
            Text = c.Text,
            Confidence = c.Confidence
        });

        return Ok(descriptions);
    }
}
