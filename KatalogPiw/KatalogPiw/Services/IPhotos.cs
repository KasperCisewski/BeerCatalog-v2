using System;
using System.Collections.Generic;
using System.Text;

namespace KatalogPiw.Services
{
    public interface IPhotos
    {
        string GetPath(string photoName);
    }
}
