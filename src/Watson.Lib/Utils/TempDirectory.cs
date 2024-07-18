namespace Watson.Lib.Utils;

using System;
using System.IO;

public class TempDirectory
{
    public static string CreateTempDirectory()
    {
        // Obtener la ruta del directorio temporal del sistema
        string tempPath = Path.GetTempPath();

        // Generar un nombre único para la nueva carpeta temporal
        string tempDirectoryName = Guid.NewGuid().ToString();

        // Combinar la ruta del directorio temporal con el nuevo nombre de carpeta
        string tempDirectoryFullPath = Path.Combine(tempPath, tempDirectoryName);

        // Crear la carpeta
        Directory.CreateDirectory(tempDirectoryFullPath);

        return tempDirectoryFullPath;
    }
}
