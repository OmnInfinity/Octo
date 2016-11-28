///// ///// ///// Octo.cs ///// ///// /////

/*
 * MIT License
 *
 * Copyright (c) 2016-2017 Kevin Pho (OmnInfinity) <kevinkhoapho@gmail.com>,
 *                         CAMS VEX 687 "Nerd Herd",
 *                         CAMS VEX 687-C
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;

/*
 * Compiler framework
 *
 * @author Kevin Pho
 */
namespace Team687.Octo
{
  class Program
  {
    public static int Success = 0;
    public static int Failure = 1;

    static int Main(string[] args)
    {
      Console.WriteLine("Octo by OmnInfinity");

      Token digit = new Token("0123456789");
      Token period = new Token(".");
      Token run = new Token().Build("run");

      Machine number = new Machine() {
        digit,
        period,
        run,
      };

      digit.start();
      digit.enter = delegate bool()
      { };



      State<string> digit = new State<string>("0213456789");
      State<string> period = new State<string>(".");
      State<string> test = new State<string>.Build("test");

      Table states = new Table() {
        { "q0", new State("q0").Start().End() },
        { "q1", new State("q1")               },
        { "q2", new State("q2")        .End() },
      };

      Machine<string> asimov = new Machine<string>().AddStates({
        digit,
        period,
        test
      });

      // The set of causes and effects for states
      Cause a = delegate (object input) { return "a".Contains((string) input); };
      Effect q0 = delegate (bool input) { return (string) "No input"; };
      Effect q2 = delegate (bool input) { return (string) "Even"; };

      // The set of transitions between states
      states["q0"].Connect(a, states["q1"], q0);
      states["q1"].Connect(a, states["q2"], q2);
      states["q2"].Connect(a, states["q1"], null);

      // The constructed state automaton
      Machine token = new Machine("Token", states);

      // Test strings through state automaton
      // token.Accepts("");
      // token.Accepts("a");
      // token.Accepts("aa");

      Console.ReadKey();
      return Program.Success;
    }

    static int Update(string[] args) {
      return Program.Success;
    }
  }
}
