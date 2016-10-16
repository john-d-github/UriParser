using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UriParser_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUriParse_Click(object sender, EventArgs e)
        {
            var uriParser = new UriParser.UriParser();
            uriParser.Parse(txtUriString.Text);

            txtScheme.Text = uriParser.Scheme;
            txtUser.Text = uriParser.User;
            txtPassword.Text = uriParser.Password;
            txtHost.Text = uriParser.Host;
            txtPort.Text = uriParser.Port;
            txtPath.Text = uriParser.Path;
            txtQuery.Text = uriParser.Query;
            txtFragment.Text = uriParser.Fragment;
        }
    }
}
