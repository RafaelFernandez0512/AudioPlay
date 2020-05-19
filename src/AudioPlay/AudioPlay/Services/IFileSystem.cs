using System;
using System.Collections.Generic;
using System.Text;

namespace AudioPlay.Services
{
    public interface IFileSystem
    {
        Dictionary<string, List<string>> GetExternalStorage();
    }
}
