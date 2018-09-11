using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QQute.Controllers
{
    public class GamesController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> GetGames( )
        {
            return new string[] { "Test","Games","List"}  ;
        }
    }
}
