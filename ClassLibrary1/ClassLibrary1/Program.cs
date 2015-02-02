using FISCA;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counsel_System_Old
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            // 匯出綜合紀錄表舊資料
            RibbonBarItem rbItem1 = MotherForm.RibbonBarItems["學生", "輔導"];
            rbItem1["匯出"].Items["匯出綜合紀錄表(舊)"].Click += delegate
            {
                if (K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0)
                {
                    ExportABCardData eap = new ExportABCardData(K12.Presentation.NLDPanels.Student.SelectedSource);
                    eap.Go();
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("請選擇學生!");
                }
            };

            // AB Card
            if (Utility.CheckAddContent(PermissionCode.綜合表現紀錄表_資料項目))
                K12.Presentation.NLDPanels.Student.AddDetailBulider<BasicInfo>();

        }
    }
}
