using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APIfp;

namespace WebGUI
{
    public partial class MainWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formulaVal = Formula.Value;
            Handler(formulaVal);
        }

        public void Handler(string formula)
        {
            
            Result.Value = formula;
        }
    }

}