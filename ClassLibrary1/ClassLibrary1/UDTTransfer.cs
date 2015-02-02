using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using System.Xml.Linq;
using FISCA.Data;
using System.Data;
using FISCA.DSAClient;
using FISCA.DSAUtil;

namespace Counsel_System_Old
{
    /// <summary>
    /// UDT 資料交換者
    /// </summary>
    public class UDTTransfer
    {
        /// <summary>
        /// 透過學生IDList取得綜合表現答案
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<UDT_ABCardDataDef> GetABCardDataListByStudentList(List<string> StudentIDList)
        {
            List<UDT_ABCardDataDef> retVal = new List<UDT_ABCardDataDef>();
            if (StudentIDList.Count > 0)
            {
                AccessHelper accessHelper = new AccessHelper();
                string qry = "ref_student_id in(" + string.Join(",", StudentIDList.ToArray()) + ")";
                retVal = accessHelper.Select<UDT_ABCardDataDef>(qry);
            }
            return retVal;
        }

        /// <summary>
        /// 取得ABCard樣板內容
        /// </summary>
        /// <returns></returns>
        public static List<UDT_ABCardTemplateDefinitionDef> GetABCardTemplate()
        {
            AccessHelper accHelper = new AccessHelper();
            List<UDT_ABCardTemplateDefinitionDef> retVal;
            retVal = accHelper.Select<UDT_ABCardTemplateDefinitionDef>();
            if (retVal == null)
                retVal = new List<UDT_ABCardTemplateDefinitionDef>();

            return retVal;
        }

        /// <summary>
        /// 新增ABCard樣板內容
        /// </summary>
        /// <returns></returns>
        public static void InsertABCardTemplate(List<UDT_ABCardTemplateDefinitionDef> RecList)
        {
            AccessHelper accHelper = new AccessHelper();
            accHelper.InsertValues(RecList);
        }

        /// <summary>
        /// 更新ABCard樣板內容
        /// </summary>
        /// <param name="RecList"></param>
        public static void UpdateABCardTemplate(List<UDT_ABCardTemplateDefinitionDef> RecList)
        {
            AccessHelper accHelper = new AccessHelper();
            accHelper.UpdateValues(RecList);
        }


        /// <summary>
        /// 刪除ABCard樣板內容
        /// </summary>
        /// <param name="RecList"></param>
        public static void DeleteABCardTemplate(List<UDT_ABCardTemplateDefinitionDef> RecList)
        {
            AccessHelper accHelper = new AccessHelper();
            foreach (UDT_ABCardTemplateDefinitionDef rec in RecList)
                rec.Deleted = true;

            accHelper.DeletedValues(RecList);
        }
    }
}
