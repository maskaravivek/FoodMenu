using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Food_Menu.Utils
{
    public class FileUtils
    {
        public async static Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        public static Type DestinationType(Type type)
        {
#if WINDOWS_PHONE_APP
            return type;
#endif
#if WINDOWS_APP
            return typeof(MainPage);
#endif
        }
    }
}
