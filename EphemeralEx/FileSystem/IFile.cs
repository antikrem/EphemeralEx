using System;
using System.Collections.Generic;
using System.IO;


namespace EphemeralEx.FileSystem
{
    public class InvalidFilePath : Exception
    {
        internal InvalidFilePath(string path)
            : base($"{path} is not a valid path for a file or directory")
        { }
    }

    public abstract class InvalidFileType<T> : Exception
        where T : IFile
    {
        public InvalidFileType(string path)
            : base($"{path} does not point to a {typeof(T).Name}")
        { }
    }

    public interface IFile
    {
        FileType Type { get; }

        IEnumerable<IFile> Children { get; }

        string Path { get; }

        string Name { get; }

        public static IFile Create(string path)
        {
            var attributes = GetAttributes(path);

            return attributes.HasFlag(FileAttributes.Directory)
                ? new Directory(new DirectoryInfo(path))
                : new File(new FileInfo(path));
        }

        internal static FileAttributes GetAttributes(string path)
        {
            try
            {
                return System.IO.File.GetAttributes(path);
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                throw new InvalidFilePath(path);
            }
        }
    }
}
