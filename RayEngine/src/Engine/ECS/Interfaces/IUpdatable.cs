using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    internal interface IUpdatable
    {
        void Update(float dt, World world);
    }
}
