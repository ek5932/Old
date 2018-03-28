using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Runner.Http
{
    public interface IHttpClient
    {
        Task<string> Get(string url);
        Task<string> Post(string url);
        Task<string> Put(string url);
        Task<string> Delete(string url);
    }
}
