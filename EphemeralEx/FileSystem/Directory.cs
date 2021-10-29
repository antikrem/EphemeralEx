using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EphemeralEx.FileSystem
{
    public class Directory : IFile
    {
        private readonly DirectoryInfo _directoryInfo;

        internal Directory(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public FileType Type => FileType.Directory;

        public IEnumerable<IFile> Children
            => ChildFiles.Concat(ChildDirectories);

        public IEnumerable<IFile> ChildFiles
            => _directoryInfo
                .EnumerateFiles()
                .Select(file => IFile.Create(file.FullName));

        public IEnumerable<IFile> ChildDirectories
            => _directoryInfo
                .EnumerateFileSystemInfos()
                .Select(file => IFile.Create(file.FullName));

        public Uri Path => new(_directoryInfo.FullName);
    }
}
