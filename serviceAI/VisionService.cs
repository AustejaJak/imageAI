using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace serviceAI;

public class VisionService
{
    private readonly string _key;
    private readonly string _endpoint;
    
    public VisionService(string key, string endpoint)
    {
        _key = key;
        _endpoint = endpoint;
    }

    public ComputerVisionClient Authenticate()
    {
        return new ComputerVisionClient(new ApiKeyServiceClientCredentials(_key))
        {
            Endpoint = _endpoint
        };
    }
}