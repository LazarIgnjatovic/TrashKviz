using Newtonsoft.Json.Linq;
using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public interface IAnswerSerializer
    {
        Answer Serialize(JObject answerJSON);
    }
}
