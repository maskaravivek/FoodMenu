using Food_Menu.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Menu.Utils
{
    public class AppUtils
    {
        public static bool AnyCounterSubscribed()
        {
            string counters = AppStore.GetValue(Constants.CountersSubscribed);
            int countersSubscribed = 0;
            if (counters != null)
            {
                countersSubscribed = Convert.ToInt32(counters);
            }
            if(countersSubscribed>0)
            {
                return true;
            }
            return false;
        }
    }
}
