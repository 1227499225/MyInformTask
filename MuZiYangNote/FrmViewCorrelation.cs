using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuZiYangNote
{
    public partial class FrmViewCorrelation : FrmBaseHostHelper
    {
        public FrmViewCorrelation()
        {
            InitializeComponent();
        }

        private void FrmViewCorrelation_Load(object sender, EventArgs e)
        {
            LaBaseTitle.Text = "报表/试图";
        }
    }
}
