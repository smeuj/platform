using System.Text.Json.Serialization;

namespace Nouwan.Smeuj.Framework
{
    public class Result<T>
    {

        private Result(ResultType resultType)
        {
            ResultType = resultType;
        }

        private Result(T payload, ResultType resultType)
        {
            Payload = payload;
            ResultType = resultType;
        }


        public static Result<T> Unprocessable(T payload) => new(payload,ResultType.Unprocessable);
        public static Result<T> Ok(T payload) => new(payload, ResultType.Ok);
        public static Result<T> NotFound() => new(ResultType.NotFound);

        public bool Successful => ResultType != ResultType.NotFound;
        public T? Payload { get; set; }
        [JsonIgnore]
        public ResultType ResultType { get; set; }
        public bool HasPayload => Payload != null;
    }

    public enum ResultType
    {
        Ok,
        Unprocessable,
        NotFound
    }
}