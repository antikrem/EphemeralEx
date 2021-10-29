using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EphemeralEx.FileSystem
{
    public class File : IFile
    {
        private readonly FileInfo _fileInfo;

        internal File(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public FileType Type => FileType.File;

        public IEnumerable<IFile> Children => Enumerable.Empty<IFile>();

        public Uri Path => new(_fileInfo.FullName);
    }
}
