using System;
using System.Web;
using System.IO;
using System.Data;
using System.Globalization;

namespace GOWEB.Models
{
    public class Statistic
    {
        public Statistic(bool State = true)
        {
            if (State)
            {
                calculate();
                online = Convert.ToInt32(HttpContext.Current.Application["Online"].ToString());
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
                DataTable TmpDt = ds.Tables[0].Copy();

                online = Convert.ToInt32(HttpContext.Current.Application["Online"].ToString());
                today = int.Parse(TmpDt.Rows[0]["today"].ToString());
                lastMonth = int.Parse(TmpDt.Rows[0]["lastMonth"].ToString());
                yesterday = int.Parse(TmpDt.Rows[0]["yesterday"].ToString());
                month = int.Parse(TmpDt.Rows[0]["month"].ToString());
                total = int.Parse(TmpDt.Rows[0]["total"].ToString());
                total = int.Parse(TmpDt.Rows[0]["total"].ToString());
            }
        }

        private int online = 0;
        public int Online
        {
            get { return online; }
        }
        private int today = 0;
        public int Today
        {
            get { return today; }
        }
        private int lastMonth = 0;
        public int LastMonth
        {
            get { return lastMonth; }
        }
        private int yesterday = 0;
        public int Yesterday
        {
            get { return yesterday; }
        }
        private int month = 0;
        public int Month
        {
            get { return month; }
        }
        private int total = 0;
        public int Total
        {
            get { return total; }
        }


        private void calculate()
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/statistics")))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/statistics"));
                Create_Xml();
                calculate();
            }
            else
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml")))
                {
                    Create_Xml();
                    calculate();
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
                    DataTable TmpDt = ds.Tables[0].Copy();

                    PersianCalendar pc = new PersianCalendar();
                    string day = pc.GetDayOfMonth(DateTime.Now).ToString("00");
                    string mnt = pc.GetMonth(DateTime.Now).ToString("00");

                    string dateMonthTmp = TmpDt.Rows[0]["dateMonth"].ToString(); // ماه جاری
                    string dateTmp = TmpDt.Rows[0]["date"].ToString(); // روز جاری



                    if (dateMonthTmp == mnt && dateTmp == day) // روز جاری ماه
                    {
                        today = int.Parse(TmpDt.Rows[0]["today"].ToString()) + 1; // جمع بازدید روز
                        lastMonth = int.Parse(TmpDt.Rows[0]["lastMonth"].ToString()); // جمع بازدید ماه قبل
                        yesterday = int.Parse(TmpDt.Rows[0]["yesterday"].ToString()); // جمع بازدید دیروز
                        month = int.Parse(TmpDt.Rows[0]["month"].ToString()) + 1; //  جمع بازدید ماه
                        total = int.Parse(TmpDt.Rows[0]["total"].ToString()) + 1; // بازدید کلی

                        TmpDt.Rows[0]["today"] = today;
                        TmpDt.Rows[0]["lastMonth"] = lastMonth;
                        TmpDt.Rows[0]["yesterday"] = yesterday;
                        TmpDt.Rows[0]["month"] = month;
                        TmpDt.Rows[0]["total"] = total;
                        TmpDt.Rows[0]["dateMonth"] = mnt;
                        TmpDt.Rows[0]["date"] = day;
                        TmpDt.Rows[0]["total"] = total;

                        TmpDt.AcceptChanges();

                        TmpDt.WriteXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
                    }
                    else if (dateMonthTmp == mnt && dateTmp != day) // روز بعدی ماه
                    {
                        lastMonth = int.Parse(TmpDt.Rows[0]["lastMonth"].ToString());
                        yesterday = int.Parse(TmpDt.Rows[0]["today"].ToString());
                        today = 1;
                        month = int.Parse(TmpDt.Rows[0]["month"].ToString()) + 1;
                        total = int.Parse(TmpDt.Rows[0]["total"].ToString()) + 1;

                        TmpDt.Rows[0]["today"] = today;
                        TmpDt.Rows[0]["lastMonth"] = lastMonth;
                        TmpDt.Rows[0]["yesterday"] = yesterday;
                        TmpDt.Rows[0]["month"] = month;
                        TmpDt.Rows[0]["dateMonth"] = mnt;
                        TmpDt.Rows[0]["date"] = day;
                        TmpDt.Rows[0]["total"] = total;

                        TmpDt.AcceptChanges();

                        TmpDt.WriteXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
                    }
                    else if (dateMonthTmp != mnt) // ماه جدید
                    {
                        lastMonth = int.Parse(TmpDt.Rows[0]["month"].ToString());
                        yesterday = int.Parse(TmpDt.Rows[0]["today"].ToString());
                        today = 1;
                        month = 1;
                        total = int.Parse(TmpDt.Rows[0]["total"].ToString()) + 1;

                        TmpDt.Rows[0]["today"] = today;
                        TmpDt.Rows[0]["lastMonth"] = lastMonth;
                        TmpDt.Rows[0]["yesterday"] = yesterday;
                        TmpDt.Rows[0]["month"] = month;
                        TmpDt.Rows[0]["dateMonth"] = mnt;
                        TmpDt.Rows[0]["date"] = day;
                        TmpDt.Rows[0]["total"] = total;

                        TmpDt.AcceptChanges();

                        TmpDt.WriteXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
                    }




                }
            }
        }
        private static void Create_Xml()
        {

            PersianCalendar pc = new PersianCalendar();
            string day = pc.GetDayOfMonth(DateTime.Now).ToString("00");
            string mnt = pc.GetMonth(DateTime.Now).ToString("00");

            DataTable Dt = new DataTable();
            Dt.Columns.Add("today"); Dt.Columns.Add("yesterday"); Dt.Columns.Add("lastmonth");
            Dt.Columns.Add("month"); Dt.Columns.Add("date"); Dt.Columns.Add("dateMonth"); Dt.Columns.Add("total");
            DataRow Dr;
            Dr = Dt.NewRow();
            Dr["month"] = "0";
            Dr["today"] = "0";
            Dr["date"] = day;
            Dr["yesterday"] = "0";
            Dr["dateMonth"] = mnt;
            Dr["lastmonth"] = "0";
            Dr["total"] = "0";
            Dt.Rows.Add(Dr);

            Dt.AcceptChanges();

            DataSet ds = new DataSet();
            ds.Tables.Add(Dt);

            ds.WriteXml(HttpContext.Current.Server.MapPath("~/statistics/statistics.xml"));
        }
    }
}