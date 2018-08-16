using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RecastAIHelper.Models;

namespace RecastAIHelper
{
    public partial class RecastAIClient : ServiceClient
    {
        public RecastAIClient(string token, string userSlug) : base(token, userSlug)
        {
        }
    }
}
