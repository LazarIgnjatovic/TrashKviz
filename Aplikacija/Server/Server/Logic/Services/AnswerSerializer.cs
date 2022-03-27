using Newtonsoft.Json.Linq;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class AnswerSerializer : IAnswerSerializer
    {
        public Answer Serialize(JObject answerJson)
        {
            Answer answer;
            if (answerJson.ContainsKey("isColumnAnswer"))
            {
                answer = answerJson.ToObject<AssociationAnswer>();
            }
            else
            {
                answer = answerJson.ToObject<Answer>();
            }
            return answer;
        }
    }
}
