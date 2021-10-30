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

        public string Name => _directoryInfo.Name;

        public IEnumerable<IFile> Children
            => ((IEnumerable<IFile>)ChildFiles)
                .Concat(ChildDirectories);

        public IEnumerable<File> ChildFiles
            => _directoryInfo
                .EnumerateFiles()
                .Select(file => (File)IFile.Create(file.FullName));

        public IEnumerable<Directory> ChildDirectories
            => _directoryInfo
                .EnumerateDirectories()
                .Select(file => (Directory)IFile.Create(file.FullName));

        public Uri Path => new(_directoryInfo.FullName);

    }
}
