using QQute.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QQute.Controllers
{
    [RoutePrefix("theapi")]
    public class GamesController : ApiController
    {
        [HttpGet]
        [CacheAF]
        [TimerAF]
        [Route("{getAllGames}")]
        public IEnumerable<string> GetGames( )
        {
            return new string[] { "Test","Games","List"}  ;
        }
    }
}
