using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Logic;
using Server.Logic.DTOs;
using Server.Logic.Masters.Match;
using Server.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Communication.Hubs
{
    [Authorize]
    public class MatchHub:Hub
    {
        private readonly IMatchService _matchService;

        public MatchHub(IMatchService matchService)
        {
            _matchService = matchService;
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _matchService.UserDisconnected(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }
        public override Task OnConnectedAsync()
        {
            string matchId = _matchService.UserConnected(Context.UserIdentifier);
            if (matchId != "")
            {
                Clients.Caller.SendAsync("MatchFound", matchId);
                Groups.AddToGroupAsync(Context.ConnectionId, matchId);
            }      
            else
                Clients.Caller.SendAsync("NoMatchFound");
            return base.OnConnectedAsync();
        }

        public async Task SubmitAnswer(string matchID, Answer answer)
        {
            _matchService.SubmitAnswer(answer, matchID, Context.UserIdentifier);
        }
        //_matchHubContext.Clients.Group(matchID).SendAsync("MatchUpdate", this);

    }
}
