using CORE.APP.Domain;

namespace CORE.APP.Features
{
    public class CommandResponse : IRecord
    {
        public bool IsSuccessful { get; }
        public string Message { get; }
        public int Id { get; set; }

        public CommandResponse(bool isSuccessful, string message = "", int id = 0)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Id = id;
        }
    }

    public class QueryResponse : IRecord
    {
        public int Id { get; set; }
    }
}
