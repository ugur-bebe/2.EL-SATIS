using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using VTYS_PROJE.Entities.Concrete;
using VTYS_PROJE.Business.Abstract;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.DAL.Abstarct;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.DAL.Concrete;

namespace Test_Form
{
    public partial class Form1 : Form
    {
        static OracleConnection con;
        public Form1()
        {
            InitializeComponent();

            con = new OracleConnection();
            con.ConnectionString = "User Id=VTYS_PROJE;Password=1234;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521)))";

        }
        class Foo
        {
            public int A { get; set; }
            public string B { get; set; }
        }

        public static int insert_return_id(string tablo, List<(string sutun, string degeri, OracleDbType dt)> values)
        {
            string sutun = String.Join(",", values.Select(x => x.sutun));
            string value = ":" + String.Join(",:", values.Select(x => x.Item1));

            List<string> whereItems = new List<string>();
            foreach (var item in values)
            {
                whereItems.Add(item.sutun + "=:" + item.sutun + "_where");
            }
            string where = String.Join(" AND ", whereItems);

            string insert_query = "Insert Into " + tablo + " (" + sutun + ") Values (" + value + ")";
            string select_query = "Select * From " + tablo + " where " + where;

            con.Open();
            OracleCommand cmd = new OracleCommand(insert_query, con);
            string s = insert_query;
            foreach (var item in values)
            {
                cmd.Parameters.Add(item.sutun, item.dt).Value = item.degeri;
                s = s.Replace(":" + item.sutun, item.degeri);
            }
            cmd.ExecuteNonQuery();

            cmd.Dispose();

            string ss = "";
            OracleCommand oracleCommand = new OracleCommand(select_query, con);
            foreach (var item in values)
            {
                oracleCommand.Parameters.Add(item.sutun + "_where", item.dt).Value = item.degeri;
                ss = ss.Replace(":" + item.sutun + "_where", item.degeri);
            }
            OracleDataReader dr = oracleCommand.ExecuteReader();

            while (dr.Read())
            {
                int id = int.Parse(dr["id"].ToString());
                con.Close();
                return id;
            }
            con.Close();

            return int.MaxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int id = insert_return_id("ADDRESS", new List<(string, string, OracleDbType)> { ("CITY_ID", "3", OracleDbType.Int16),
                                                                                         ("DISTRICT_ID", "1", OracleDbType.Int16),
                                                                                         ("ADDRESS_DESCRIPTION", "Deneme açıklama", OracleDbType.Varchar2)});

            MessageBox.Show(id.ToString());

            return;
            ILogManager logManager = new LogManager();
            ProductProperty productProperty = new ProductProperty();
            NoSqlDal noSqlDal = new NoSqlDal(logManager);

            var x = noSqlDal.GetById(textBox1.Text);



            /* productProperty.images = new List<string>() { "deneme" };
             productProperty.image_id = "123123";

             noSqlDal.Insert(ref productProperty);*/

            return;
            string iString = "2005-05-05";
            DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd", null);
            MessageBox.Show(oDate.ToString());

            string format = "yyyy-MM-dd hh:mm:ss";
            string dateTime = DateTime.Now.ToString(format);
            DateTime d = DateTime.ParseExact(DateTime.Now.ToString(), "d", null);

            DateTime myDate = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            DateTime Dd = Convert.ToDateTime(dateTime);

            List<User> u = new List<User>();

            OracleCommand c = new OracleCommand();
            var q =
            from cust in u
            where cust.user_name == "London"
            select cust;

            var translator = new MyQueryTranslator();
            //string whereClause = translator.Translate();

            //DbCommand dc = db.GetCommand(q);
            try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = "User Id=SYSTEM;Password=1234;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521)))";
                con.Open();

                MessageBox.Show("Bağlantı Başarılı!");
                con.Clone();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı Başarısız!\n" + ex);
            }
        }
    }
}
