using System;
using System.Collections.Generic;
using System.Text;
using TestTool.Core;

namespace TestTool.Runner.Http
{
    public interface IJsonResultParser
    {
        IActualContent ParseJson(string results);
    }
}
