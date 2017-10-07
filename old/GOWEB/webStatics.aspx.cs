using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GOWEB
{
    public partial class webStatics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //   if (Request.Cookies["amar"] == null)
            //   {
            if (Session["Login"].ToString().Equals("1"))
            {
                Statistic st = new Statistic(true);
                Label1.Text = "تعداد افرادآنلاين : " + st.Online;
                Label2.Text = "تعداد بازديد امروز : " + st.Today;
                Label3.Text = "تعداد بازديد ديروز : " + st.Yesterday;
                Label4.Text = "تعداد بازديد ماه جاری : " + st.Month;
                Label5.Text = "تعداد بازديد ماه گذشته : " + st.LastMonth;
                Label6.Text = "تعداد بازديد كل : " + st.Total;
                Session["Login"] = "0";
            }
            else
            {
                Statistic st = new Statistic(false);
                Label1.Text = "تعداد افرادآنلاين : " + st.Online;
                Label2.Text = "تعداد بازديد امروز : " + st.Today;
                Label3.Text = "تعداد بازديد ديروز : " + st.Yesterday;
                Label4.Text = "تعداد بازديد ماه جاری : " + st.Month;
                Label5.Text = "تعداد بازديد ماه گذشته : " + st.LastMonth;
                Label6.Text = "تعداد بازديد كل : " + st.Total;
            }
            // HttpCookie cooki = new HttpCookie("amar");
            // cooki.Expires = DateTime.Now.AddHours(1);
            // cooki.Value = "true";
            // Response.Cookies.Add(cooki);
            //   }
            //}
        }



        protected void Button1_Click(object sender, EventArgs e)
        {

        }

    }
}