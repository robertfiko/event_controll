using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboChase.Model;

namespace RoboChase.Persistance
{
    public interface IRoboChaseData
    {
        Task<RoboChaseInfo> LoadAsync(String path);
        Task<bool> SaveAsync(String path, RoboChaseInfo info);
    }
}
