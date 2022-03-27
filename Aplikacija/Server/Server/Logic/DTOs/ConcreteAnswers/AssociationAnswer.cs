using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.DTOs.ConcreteAnswers
{
    public class AssociationAnswer : Answer
    {
        
        public int Column;
        public int Field;
        public bool IsColumnAnswer;
        public bool IsFinalAnswer;
    }
}
