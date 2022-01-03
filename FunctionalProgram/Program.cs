using System;
using System.Linq;
using System.Text;

namespace FunctionalProgram;

class Program {
    static void Main(string[] args) {
        Disposable
            .Using(
                StreamFactory.GetStream,
                stream => {
                    var b = new byte[stream.Length];
                    stream.Read(b, 0, b.Length);
                    return b;
                })
            .Map(Encoding.UTF8.GetString)
            .Split(new string[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select((s, i) => Tuple.Create(i, s))
            .ToDictionary(t => t.Item1, t => t.Item2)
            .Map(options => HtmlBuilder.BuildSelectBox("theDoctors", true));

        // Transformation Pipeline
        Disposable
            .Using(
                StreamFactory.GetStream,
                stream => new byte[stream.Length].Tee(b => stream.Read(b, 0, (int)stream.Length)))
            .Map(Encoding.UTF8.GetString)
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .Select((s, ix) => Tuple.Create(ix, s))
            .ToDictionary(k => k.Item1, v => v.Item2)
            .Map(HtmlBuilder.BuildSelectBox("theDoctors", true))
            .Tee(Console.WriteLine);

        string s1 = "null";
        var r = Validator.IsNotNull(s1);

        // String Validation Pipeline
        "Doctor Who"
            .Map(Validator.IsNotNull)
            .Bind(Validator.IsNotEmpty)
            .Bind(Validator.MinLength(8))
            .Map(result => result.IsSuccess ? result.SuccessValue : result.FailureValue)
            .Tee(Console.WriteLine);

        Console.ReadLine();
    }
}
