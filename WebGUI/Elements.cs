using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIfp;
using System.ComponentModel.DataAnnotations;

namespace WebGUI
{
    public class Elements
    {
        [Required]
        public string Formula { get; set; }

        public string Result { get; set; }
    }
}