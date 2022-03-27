using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logic.Services
{
    public class StandardStringService : IStandardStringService
    {
        public string Standardize(string latinString)
        {
            string retVal = latinString;
            retVal= retVal.ToLower();
            for(int i=0;i<retVal.Length;i++)
            {
                string substitute;
                switch(retVal[i])
                {
                    case 'č':
                        substitute = "c";
                        break;
                    case 'ć':
                        substitute = "c";
                        break;
                    case 'š':
                        substitute = "s";
                        break;
                    case 'đ':
                        substitute = "dj";
                        break;
                    case 'ž':
                        substitute = "z";
                        break;
                    default:
                        substitute = retVal[i].ToString();
                        break;
                }
                retVal = retVal.Remove(i, 1);
                retVal = retVal.Insert(i, substitute);
            }
            return retVal;
        }
    }
}
