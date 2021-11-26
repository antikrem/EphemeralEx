using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace EphemeralEx.FileSystem
{
    public class NotADirectoryException : InvalidFileType<Directory>
    {
        public NotADirectoryException(string path)
            : base(path)
        { }
    }

    public class Directory : IFile
    {
        private readonly DirectoryInfo _directoryInfo;

        internal Directory(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public static Directory Create(string path)
        {
            var attributes = IFile.GetAttributes(path);

            return attributes.HasFlag(FileAttributes.Directory)
                ? new Directory(new DirectoryInfo(path))
                : throw new NotADirectoryException(path);
        }

        public FileType Type => FileType.Directory;

        public string Name => _directoryInfo.Name;

        public IEnumerable<IFile> Children
            => ((IEnumerable<IFile>)ChildFiles)
                .Concat(ChildDirectories);

        public IEnumerable<File> ChildFiles
            => _directoryInfo
                .EnumerateFiles()
                .Select(file => File.Create(file.FullName));

        public IEnumerable<Directory> ChildDirectories
            => _directoryInfo
                .EnumerateDirectories()
                .Select(file => Directory.Create(file.FullName));

        public string Path => _directoryInfo.FullName;

    }
}
