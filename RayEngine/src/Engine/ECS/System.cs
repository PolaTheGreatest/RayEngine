using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RayEngine
{
    public abstract class System
    {
        public virtual string ID { get; protected set; } = "UnnamedSystem";

        public abstract void Update(float dt, World world);
    }
}
