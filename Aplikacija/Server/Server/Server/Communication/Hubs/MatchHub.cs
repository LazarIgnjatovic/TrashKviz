﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Logic;
using Server.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatchHub:Hub
    {
        public async Task JoinMatch(string matchID)
        {
            //todo slanje celog meca je temp
            Match match = new Match();
            await Groups.AddToGroupAsync(Context.ConnectionId, matchID);
            await Clients.Group(matchID).SendAsync("MatchUpdate", match);
        }
        public async Task SubmitAnswer(string matchID, Answer answer)
        {
            //todo obrada odgovora
            Match match = new Match();
            await Clients.Group(matchID).SendAsync("MatchUpdate", match);
        }
        public async Task MatchUpdate(Match match)
        {
            //server interno poziva
            //temp
            await Clients.Group(match.MatchID).SendAsync("MatchUpdate", match);
        }
        public async Task DeclareWinner(UserDTO winner,string matchID)
        {
            await Clients.Group(matchID).SendAsync("MatchWinner", winner);
            //todo zatvaranje igre
        }
        public async Task LeaveMatch(string matchID)
        {
            Match match = new Match();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, matchID);
            await Clients.Group(match.MatchID).SendAsync("MatchUpdate", match);
        }

    }
}