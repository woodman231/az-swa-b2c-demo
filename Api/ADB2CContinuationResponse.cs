using Newtonsoft.Json;

namespace Api;

public class ADB2CContinuationResponse
{
    public const string ApiVersion = "1.0.0";
    public const string Action = "Continue";

    public ADB2CContinuationResponse()
    {
        version = ADB2CContinuationResponse.ApiVersion;
        action = ADB2CContinuationResponse.Action;                
    }

    public ADB2CContinuationResponse(string newPostalCode)
    {
        version = ADB2CContinuationResponse.ApiVersion;
        action = ADB2CContinuationResponse.Action;
        postalCode = newPostalCode;
    }

    public string version {get;}
    public string action {get;}
    
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string postalCode {get; set;}

}