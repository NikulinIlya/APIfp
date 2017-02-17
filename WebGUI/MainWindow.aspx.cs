using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APIfp;
using System.Threading;

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
            //parser
            var formulaArgs = new List<string>();
            string arg = "";
            for (int i =0;i< formula.Length; i++)
            {
                if (!formula[i].Equals(".")) arg += formula[i];
                else
                {
                    formulaArgs.Add(arg);
                    arg = "";
                }
            }

            Result.Value = formula;
        }
    }

}