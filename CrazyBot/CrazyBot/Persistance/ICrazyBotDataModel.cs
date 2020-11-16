using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrazyBot.Model;

namespace CrazyBot.Persistance
{
    public interface ICrazyBotDataModel
    {
        CrazyBotInfo Load(String path);
        void Save(String path, CrazyBotInfo info);
    }
}
