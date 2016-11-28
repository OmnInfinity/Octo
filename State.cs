///// ///// ///// State.cs ///// ///// /////

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

namespace Team687.Octo
{
  public delegate bool Cause(object input);

  public delegate object Effect(bool input);

  public interface Connectable<T>
  {
    T Connect(Cause choice, T output, Effect result);
  }

  public class Table : Dictionary<string, State> { }

  public class Machine
  {
    public string Name { get; set; }
    public Table States { get; set; }
    public List<string> Queue { get; set; }

    public Machine(string name, Table states)
    {
      this.Name = name;
      this.States = states;
      this.Queue = new List<string>();

      foreach (State state in new List<State>(states.Values))
      {
        if (state.Acidic)
        {
          this.Queue.Add(state.Name);
        }
      }
    }

    /* TODO */
    // public bool Traverse(T ype) { return true; }

    /* TODO */
    // public bool Accepts(T ype) { return true; }
  }

  public class State : Connectable<State>
  {
    public string Name { get; set; }
    public int Priority { get; set; }
    public List<Tuple<Cause, State, Effect>> Connections { get; set; }
    public bool Acidic { get; set; }
    public bool Basic { get; set; }
    public delegate void Entered();
    public delegate void Exited();

    public State(string name, int priority = 0)
    {
      this.Name = name;
      this.Priority = priority;
      this.Connections = new List<Tuple<Cause, State, Effect>>();
      this.Acidic = false;
      this.Basic = false;
    }

    public State Start()
    {
      this.Acidic = true;
      return this;
    }

    public State End()
    {
      this.Basic = true;
      return this;
    }

    public State Connect(Cause choice, State output, Effect result)
    {
      Tuple<Cause, State, Effect> connection = new Tuple<Cause, State, Effect>(choice, output, result);

      if (!this.Connections.Contains(connection))
      {
        this.Connections.Add(connection);
      }

      return this;
    }
  }
}
