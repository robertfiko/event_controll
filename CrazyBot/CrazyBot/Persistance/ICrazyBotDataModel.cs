using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CrazyBot.Model;

namespace CrazyBot.Persistance
{
    interface ICrazyBotDataModel
    {
        Task<CrazyBotInfo> LoadAsync(String path);
        Task SaveAsync(String path, CrazyBotInfo info);
    }
}
