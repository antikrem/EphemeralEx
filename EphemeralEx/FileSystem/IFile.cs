using System;
using System.Collections.Generic;
using System.IO;


namespace EphemeralEx.FileSystem
{
    public class InvalidFileURL : Exception
    {
        internal InvalidFileURL(string path)
            : base($"{path} is not a valid path for a file or directory")
        { }
    }

    public interface IFile
    {
        FileType Type { get; }

        IEnumerable<IFile> Children { get; }

        Uri Path { get; }

        string Name { get; }

        public static IFile Create(string path)
        {
            var attributes = GetAttributes(path);

            return attributes.HasFlag(FileAttributes.Directory)
                ? new Directory(new DirectoryInfo(path))
                : new File(new FileInfo(path));
        }

        private static FileAttributes GetAttributes(string path)
        {
            try
            {
                return System.IO.File.GetAttributes(path);
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                throw new InvalidFileURL(path);
            }
        }
    }
}
