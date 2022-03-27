using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Logic;
using Server.Logic.DTOs;
using Server.Logic.DTOs.ConcreteAnswers;
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
        private readonly IAnswerSerializer _answerSerializer;

        public MatchHub(IMatchService matchService,IAnswerSerializer answerSerializer)
        {
            _matchService = matchService;
            _answerSerializer = answerSerializer;
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

        public async Task SubmitAnswer(string matchID, JObject answerJson)
        {
            Answer answer = _answerSerializer.Serialize(answerJson);
            _matchService.SubmitAnswer(answer, matchID, Context.UserIdentifier);
        }
        //_matchHubContext.Clients.Group(matchID).SendAsync("MatchUpdate", this);
        //_matchHubContext.Clients.Group(matchID).SendAsync("Tick");
        //_matchHubContext.Clients.User(username).SendAsync("OnTurn");
        //_matchHubContext.Clients.User(username).SendAsync("NotOnTurn");
        //_matchHubContext.Clients.Group(matchID).SendAsync("MatchEnd", players);

    }
}
