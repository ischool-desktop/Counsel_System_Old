using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Counsel_System_Old
{
    public partial class SetABTemplateXMLForm : FISCA.Presentation.Controls.BaseForm
    {
        List<UDT_ABCardTemplateDefinitionDef> ABCardTemplate;

        public SetABTemplateXMLForm()
        {
            InitializeComponent();
            this.MaximumSize = this.MinimumSize = this.Size;
            ABCardTemplate = new List<UDT_ABCardTemplateDefinitionDef>();
        }

        private void dgSetTemplate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            dgSetTemplate.Rows.Clear();
            ABCardTemplate = UDTTransfer.GetABCardTemplate().OrderBy(x => x.SubjectName).ToList();
            foreach (UDT_ABCardTemplateDefinitionDef data in ABCardTemplate)
            {
                int row = dgSetTemplate.Rows.Add();
                dgSetTemplate.Rows[row].Tag = data;                
                dgSetTemplate.Rows[row].Cells[colName.Index].Value = data.SubjectName;
                dgSetTemplate.Rows[row].Cells[colXML.Index].Value = data.Content;            
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<UDT_ABCardTemplateDefinitionDef> updateList = new List<UDT_ABCardTemplateDefinitionDef>();
            List<UDT_ABCardTemplateDefinitionDef> insertrList = new List<UDT_ABCardTemplateDefinitionDef>();
            List<UDT_ABCardTemplateDefinitionDef> deleteList = new List<UDT_ABCardTemplateDefinitionDef>();
            List<string> UIDList = new List<string>();
            foreach (DataGridViewRow dgvr in dgSetTemplate.Rows)
            {
                if (dgvr.IsNewRow)
                    continue;
                UDT_ABCardTemplateDefinitionDef data = dgvr.Tag as UDT_ABCardTemplateDefinitionDef;
                if (data != null)
                {
                    if (dgvr.Cells[colXML.Index].Value != null)
                        data.Content = dgvr.Cells[colXML.Index].Value.ToString();
                    else
                        data.Content = "";

                    if (dgvr.Cells[colName.Index].Value != null)
                        data.SubjectName = dgvr.Cells[colName.Index].Value.ToString();
                    else
                        data.SubjectName = "";

                    if (!string.IsNullOrEmpty(data.UID))
                    {
                        updateList.Add(data);
                        UIDList.Add(data.UID);
                    }
                }
                else
                {
                    UDT_ABCardTemplateDefinitionDef dataI = new UDT_ABCardTemplateDefinitionDef();
                    if (dgvr.Cells[colXML.Index].Value != null)
                        dataI.Content = dgvr.Cells[colXML.Index].Value.ToString();
                    else
                        dataI.Content = "";

                    if (dgvr.Cells[colName.Index].Value != null)
                        dataI.SubjectName = dgvr.Cells[colName.Index].Value.ToString();
                    else
                        dataI.SubjectName = "";

                    insertrList.Add(dataI);
                }
                
            }

            // delete
            foreach (UDT_ABCardTemplateDefinitionDef data in ABCardTemplate)
            {
                if (!UIDList.Contains(data.UID))
                {
                    data.Deleted = true;
                    deleteList.Add(data);
                }            
            }

            if (deleteList.Count > 0)
                UDTTransfer.DeleteABCardTemplate(deleteList);

            if (updateList.Count > 0 || insertrList.Count >0)
            {
                if(updateList.Count >0)                
                    UDTTransfer.UpdateABCardTemplate(updateList);

                if (insertrList.Count > 0)
                    UDTTransfer.InsertABCardTemplate(insertrList);

                FISCA.Presentation.Controls.MsgBox.Show("儲存完成.");
            }

            btnSave.Enabled = false;
        }

        private void SetABTemplateXMLForm_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }
    }
}
