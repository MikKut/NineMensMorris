using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NineMensMorris.FilesOperations
{
    internal static class Paths
    {
        public readonly static string
            pathFromBootDirectoryToStartProjDir =
           Directory.GetCurrentDirectory().Replace(PathFromStartProjFolderToSolutionToExeFile, ""),
            pathOfTheCounter =
           pathFromBootDirectoryToStartProjDir + @$"{PathFromStartProjFolderToSaves}\{NameOfTheFileWithCounter}",
            pathOfTheNames =
           pathFromBootDirectoryToStartProjDir + @$"{PathFromStartProjFolderToSaves}\{NameOfTheFileWithNames}";
        public const string
            NameOfTheFileWithNames = "Names.txt",
            NameOfTheFileWithCounter = "Counter.txt",
            PathFromStartProjFolderToSolutionToExeFile = @"\bin\Debug\net6.0-windows",
            PathFromStartProjFolderToSaves = @"\FilesOperations\Saves";

    }

}
