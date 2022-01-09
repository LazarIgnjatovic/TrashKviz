using Server.Logic.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Business.Games
{
    public interface IGame
    {
        public void SubmitAnswer(Answer answer);
        //todo dodati ostale potrebne funkcije
    }
}
