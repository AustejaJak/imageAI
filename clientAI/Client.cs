namespace clientAI;

public class Client
{
    public int ClientId { get; set; }
    private List<Guid> SentRequestsIds = new();
    private List<Guid> EmptyResultsIds = new();
    private DateTime NextSendTime;
    private DateTime NextRequestTime;
    private Random rng = new();

    public Client(int clientId, DateTime startTime)
    {
        ClientId = clientId;
        NextSendTime = startTime;
        NextRequestTime = startTime;
    }

    public List<(int clientId, Guid requestId, string data)> TryGeneratePacket(DateTime currentTime)
    {
        if (currentTime < NextSendTime) return new();

        int n = rng.Next(1, 10);
        var packet = new List<(int, Guid, string)>();

        for (int i = 0; i < n; i++)
        {
            var requestId = Guid.NewGuid();
            var data = $"Data_{ClientId}_{i}"; //should be a random link
            SentRequestsIds.Add(requestId);
            packet.Add((ClientId, requestId, data));
        }

        NextSendTime = currentTime + TimeSpan.FromSeconds(rng.Next(1, 5));
        return packet;
    }

    public (int clientId, Guid resultRequestId)? TryGenerateResultQuery(DateTime currentTime)
    {
        if (currentTime < NextRequestTime) return null;
        
        Guid resultRequestId;
        if (EmptyResultsIds.Count > 0)
        {
            resultRequestId = EmptyResultsIds[rng.Next(EmptyResultsIds.Count)];
        } 
        else if (SentRequestsIds.Count > 0)
        {
            resultRequestId = SentRequestsIds[rng.Next(SentRequestsIds.Count)];
        }
        else
        {
            return null;
        }
        
        NextRequestTime = currentTime + TimeSpan.FromSeconds(rng.Next(1, 5));
        return (ClientId, resultRequestId);
    }

    public void ReceiveAnswer(Guid resultRequestId, string? result)
    {
        if (result != null)
        {
            SentRequestsIds.Remove(resultRequestId);
            EmptyResultsIds.Remove(resultRequestId);
        }
        else
        {
            EmptyResultsIds.Add(resultRequestId);
        }
    }
    
    public (int clientId, Guid resultRequestId) GenerateY2()
    {
        if (EmptyResultsIds.Count > 0)
        {
            return (ClientId, EmptyResultsIds[0]);
        }
        else if (SentRequestsIds.Count > 0)
        {
            var k = SentRequestsIds[rng.Next(SentRequestsIds.Count)];
            return (ClientId, k);
        }
        else
        {
            var randomK = Guid.NewGuid();
            return (ClientId, randomK);
        }
    }

}