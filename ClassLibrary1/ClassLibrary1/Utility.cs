using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Counsel_System_Old
{
    class Utility
    {
        /// <summary>
        /// 檢查是否加入資料項目
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CheckAddContent(string code)
        {
            bool retVal = false;
            if (FISCA.Permission.UserAcl.Current[code].Viewable)
                retVal = true;

            return retVal;
        }
    }
}
