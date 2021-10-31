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

        public string Name => _fileInfo.Name;

        public string Extension => _fileInfo.Extension;

        public IEnumerable<IFile> Children => Enumerable.Empty<IFile>();

        public string Path => _fileInfo.FullName;
    }
}
