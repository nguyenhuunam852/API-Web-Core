using MyWebApp.Shared;
using System.Text.Json.Serialization;

namespace MyWebApp.DTO
{
    public class CustomResponse
    {
        public CustomResponse(int errorCode = 0, List<string>? errorsList = null,Object? content = null)
        {
            ErrorsList = errorsList;
            ErrorCode = errorCode;
            ErrorMessage = ErrorsListDictionary.Errors.GetValueOrDefault(errorCode);
            Content = content;
        }

        public CustomResponse(Tuple<int,List<string>> tupleError)
        {
            ErrorsList = tupleError.Item2;
            ErrorCode = tupleError.Item1;
            ErrorMessage = ErrorsListDictionary.Errors.GetValueOrDefault(tupleError.Item1);
        }

        public int ErrorCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ErrorMessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string>? ErrorsList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Object? Content { get; set; }
    }
}