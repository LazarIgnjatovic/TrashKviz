using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Games
{
    public interface IGame
    {
        void SubmitAnswer(Answer answer,string username);
        GameState GetState();
        void FlagConnected(string username);
        void FlagDisconnected(string username);
        void Quit();
    }
}
