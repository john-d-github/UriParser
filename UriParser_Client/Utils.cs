using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UriParser_Client
{
    class Utils
    {
        static public void CopyListBox(ListBox lst, Form frm)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder();

            try
            {
                frm.Cursor = Cursors.WaitCursor;

                // copy data 
                foreach (string item in lst.Items)
                {
                    sb.Append(item);
                    sb.Append(Environment.NewLine);
                    count++;
                }

                // put in clipboard
                Clipboard.SetDataObject(new DataObject(sb.ToString()), true);
                MessageBox.Show(string.Format("Copied {0} record(s) to the clipboard [{1} characters].", count, sb.Length));
            }
            catch (Exception ex)
            {
                MessageBox.Show(frm, "Failed to copy: " + ex.Message, "Failed to copy");
            }
            finally
            {
                frm.Cursor = Cursors.Default;
            }
        }

        static public void CopyListViewItems(ListView lvw, Form frm)
        {
            int count = 0;
            StringBuilder sb = new StringBuilder();

            try
            {
                frm.Cursor = Cursors.WaitCursor;

                // copy headers
                for (int i = 0; i < lvw.Columns.Count; i++)
                {
                    sb.Append(lvw.Columns[i].Text);
                    if (i < lvw.Columns.Count - 1)
                        sb.Append('\t');
                }
                sb.Append(Environment.NewLine);

                // copy data 
                foreach (ListViewItem item in lvw.Items)
                {
                    for (int i = 0; i < lvw.Items[0].SubItems.Count; i++)
                    {
                        sb.Append(item.SubItems[i].Text);
                        if (i < lvw.Items[0].SubItems.Count - 1)
                            sb.Append('\t');
                    }
                    sb.Append(Environment.NewLine);
                    count++;
                }

                // put in clipboard
                Clipboard.SetDataObject(new DataObject(sb.ToString()), true);
                MessageBox.Show(string.Format("Copied {0} record(s) to the clipboard [{1} characters].", count, sb.Length));
            }
            catch (Exception ex)
            {
                MessageBox.Show(frm, "Failed to copy: " + ex.Message, "Failed to copy");
            }
            finally
            {
                frm.Cursor = Cursors.Default;
            }
        }


    }
}
