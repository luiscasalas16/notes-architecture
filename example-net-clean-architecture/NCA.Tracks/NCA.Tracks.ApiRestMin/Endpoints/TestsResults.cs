using NCA.Common.Domain.Models;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class TestsResults : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            // csharpier-ignore
            app.Group(this)

                .Get("ResultSuccess", () => Result.Success())

                .Get("ResultSuccessInt", () => Result<int>.Success(1))

                .Get("ResultSuccessString", () => Result<string>.Success("hello world!"))

                .Get("ResultSuccessObject", () =>
                    Result<Result_Test>.Success(new Result_Test
                    {
                        VInt = 1,
                        VString = "hello world!",
                        VDictionary = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } },
                        VList = [new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } }, new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } }]
                    }))

                .Get("ResultFailureError", () => Result.Failure(new Error("error.code.1", "Error messaje 1.")))

                .Get("ResultFailureErrors", () => Result.Failure([new Error("error.code.1", "Error messaje 1."), new Error("error.code.2", "Error messaje 2.")]))

                .Get("ResultException", () => { throw new ApplicationException("test exception"); })

                .Get("ProblemResult", () => TypedResults.Problem
                    (
                        statusCode: 400,
                        title: "One or more validation errors occurred."
                    ))

                .Get("ValidationProblemResult", () => TypedResults.ValidationProblem
                    (
                        title: "One or more validation errors occurred.",
                        errors: new Dictionary<string, string[]>
                        {
                            { "Parameter1" , [ "Cannot be null.", "Cannot be blank."] },
                            { "Parameter2" , [ "Cannot be null.", "Cannot be blank."] }
                        }
                    ))

                ;
        }

        public class Result_Test
        {
            public int VInt { get; set; }
            public string? VString { get; set; }
            public Dictionary<string, string>? VDictionary { get; set; }
            public List<Dictionary<string, string>>? VList { get; set; }
        }
    }
}
