using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.InfaStructure.Data
{
    public interface IDBInitializer
    {
        void SeedDb(LibraryAppContext ctx);
    }
}
