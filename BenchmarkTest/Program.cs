using System;
using Microsoft.AspNetCore.Http;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkTest
{
    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run<Example>();
        }
    }

    public class Example {
        public Example() {
            AddShortString();
              AddLongString();
        }
        
        private PathString _first = new PathString("/first/");
        private PathString _second = new PathString("/second/");
        private PathString _long = new PathString("/longerpathstringtoshowsubstring/");

        [Benchmark]
        public PathString AddShortString() {
            return _first.Add(_second);
        }
        
        [Benchmark]
        public PathString AddLongString() {
            return _first.Add(_long);
        }
    }
}
