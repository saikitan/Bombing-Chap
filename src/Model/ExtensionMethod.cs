using System;
using System.IO;

namespace MyGame
{
    /// <summary>
    ///     Extension Method for the file reader
    /// </summary>
    public static class ExtensionMethod
    {
        public static int ReadInteger (this StreamReader reader)
        {
            return Convert.ToInt32 (reader.ReadLine ());
        }
    }
}
