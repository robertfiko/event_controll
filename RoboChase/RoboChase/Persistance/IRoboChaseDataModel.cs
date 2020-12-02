using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboChase.Model;

namespace RoboChase.Persistance
{
    public interface IRoboChaseDataModel
    {
        RoboChaseInfo Load(String path);
        void Save(String path, RoboChaseInfo info);
    }
}
