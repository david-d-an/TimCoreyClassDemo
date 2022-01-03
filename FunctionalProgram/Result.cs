using System;

namespace FunctionalProgram;

public class Result<TSuccess, TFailure> {
    // private readonly bool _isSuccess;

    private Result(bool isSuccess){
        // _isSuccess = isSuccess;
        IsSuccess = isSuccess;
    }

    public TSuccess SuccessValue { get; private set; }
    public TFailure FailureValue { get; private set; }
    // public bool IsSuccess { get { return _isSuccess; } }
    public bool IsSuccess { get; private init; }

    public static Result<TSuccess, TFailure> SucceedWith(TSuccess value) =>
        new Result<TSuccess, TFailure>(true) {
            SuccessValue = value
        };

    public static Result<TSuccess, TFailure> FailWith(TFailure value) =>
        new Result<TSuccess, TFailure>(false) {
            FailureValue = value
        };

    public Result<TSuccess, TFailure> Bind(Func<TSuccess, Result<TSuccess, TFailure>> fn) =>
        this.IsSuccess
            ? fn(this.SuccessValue)
            : this;
}

// public static class ResultHelp {
//     public static Result<string, string> Bind2(this Result<string, string> @this, 
//                                             Func<string, Result<string, string>> fn) =>
//         @this.IsSuccess
//             ? fn(@this.SuccessValue)
//             : @this;
// }

