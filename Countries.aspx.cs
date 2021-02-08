using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebAppCountries
{
    public partial class Countries : System.Web.UI.Page
    {
        SqlConnection cn = new SqlConnection("Data Source=DESKTOP-6IDKG38;Initial Catalog=WFA3DotNet;Integrated Security=True");
        public void Bind_ddlCountry()
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from Country", cn);
            SqlDataReader dr = cmd.ExecuteReader();
            ddlCountry.DataSource = dr;
            ddlCountry.Items.Clear();
            
            ddlCountry.DataTextField = "Cname";
            ddlCountry.DataValueField = "Cid";
            ddlCountry.DataBind();
            cn.Close();
        }
        public void Bind_ddlState()
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand("select Sid,Sname from State where Cid='" + ddlCountry.SelectedValue + "'", cn);

            SqlDataReader dr = cmd.ExecuteReader();
            ddlState.DataSource = dr;
            ddlState.Items.Clear();
            ddlState.Items.Add("--Please Select state--");
            ddlState.DataTextField = "Sname";
            ddlState.DataValueField = "Sid";
            ddlState.DataBind();
            cn.Close();
        }
        public void Bind_ddlCity()
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select CityId,CityName from City where Sid ='" + ddlState.SelectedValue + "'" , cn);

            SqlDataReader dr = cmd.ExecuteReader();
            ddlCity.DataSource = dr;
            ddlCity.Items.Clear();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityID";
            ddlCity.DataBind();
            cn.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_ddlCountry();
            }

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlState();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlCity();
        }
    }
}