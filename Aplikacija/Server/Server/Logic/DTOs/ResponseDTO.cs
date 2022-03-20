using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs
{
    public class ResponseDTO
    {
        public string Message { get; set; }

        public ResponseDTO(string message)
        {
            Message = message;
        }
    }
}
