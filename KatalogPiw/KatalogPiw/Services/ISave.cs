using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KatalogPiw.Services
{
    public interface ISave
    {
        Task SaveTextAsync(string filename, string contentType, MemoryStream s);

    }
}
