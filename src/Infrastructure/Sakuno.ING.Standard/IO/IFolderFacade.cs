﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sakuno.ING.IO
{
    public interface IFolderFacade : IFileSystemFacade
    {
        ValueTask<IFileFacade> GetFileAsync(string filename);
        ValueTask<IFolderFacade> GetFolderAsync(string foldername);
        ValueTask<IEnumerable<IFileFacade>> GetFilesAsync();
    }
}
