using DatabaseProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace KanziiImport
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            List<KanziiObject> lsAllKanzii = new List<KanziiObject>();

            for (int i = 1; i <= 32; i++)
            {
                List<KanziiObject> lsKanzii = QueryMazii(1, 100, i);
                lsAllKanzii.AddRange(lsKanzii);
            }

            InsertDatabase(lsAllKanzii);
        }

        private List<KanziiObject> QueryMazii(int from, int to, int lessonId)
        {
            const String URI = "http://mina.mazii.net/api/getKanji.php";
            String parameter = "";
            //if (lessonId > 0)
            //{
                parameter = lessonId.ToString();
            //} else
            //{
            //    parameter = String.Format("from={0}&to={1}", from, to);
            //}

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string htmlResult = wc.DownloadString(String.Format("{0}?{1}", URI, parameter));
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<KanziiObject> lsKanzii = jss.Deserialize<List<KanziiObject>>(htmlResult);
                return lsKanzii;
            }
        }

        private void InsertDatabase(List<KanziiObject> lsKanzii)
        {
            DbAccess db = new DbAccess();
            db.OpenConnection();

            try
            {
                lsKanzii.ForEach(kanzii =>
                {
                    db.InsertKanzii(kanzii);
                });
            } finally
            {
                db.CloseCommitConnection();
            }
        }
    }
}
