using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace EphemeralEx.FileSystem
{
    public class NotAFileException : InvalidFileType<File>
    {
        public NotAFileException(string path)
            : base(path)
        { }
    }

    public class File : IFile
    {
        private readonly FileInfo _fileInfo;

        internal File(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public static File Create(string path)
        {
            var attributes = IFile.GetAttributes(path);

            return attributes.HasFlag(FileAttributes.Directory)
                ? throw new NotAFileException(path)
                : new File(new FileInfo(path));
        }

        public FileType Type => FileType.File;

        public string Name => _fileInfo.Name;

        public string Extension => _fileInfo.Extension;

        public IEnumerable<IFile> Children => Enumerable.Empty<IFile>();

        public string Path => _fileInfo.FullName;
    }
}
