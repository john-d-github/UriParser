using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            txtError.Text = uriParser.Error;
        }

    // Parse a URI into its components
        private class UriTestCase
        {
            public string UriString;
            public string Scheme;
            public string User;
            public string Password;
            public string Host;
            public string Port;
            public string Path;
            public string Query;
            public string Fragment;
            public string Error;

            public string ToString()
            {
                return (String.Format("UriString={0} Scheme={1} User={2} Password={3} Host={4) Port={5} Path={6} Query={7} Fragment={8} Error={9}", 
                    UriString, Scheme, User, Password, Host, Port, Path, Query, Fragment, Error));
            }
        }

        private void cmdTestCases_Click(object sender, EventArgs e)
        {
            var testCases = new List<UriTestCase>();
            string line;
            StreamReader file = new StreamReader(@"uri-test-cases.txt");
            var item = new UriTestCase();
            while ((line = file.ReadLine()) != null)
            {
                var fields = line.Split(new char[] { '\t' });
                item.UriString = fields[0];
                item.Scheme = fields[1];
                item.User = fields[2];
                item.Password = fields[3];
                item.Host = fields[4];
                item.Port = fields[5];
                item.Path = fields[6];
                item.Query = fields[7];
                item.Fragment = fields[8];
                item.Error = fields[9];
                testCases.Add(item);
                item = new UriTestCase();
            }

            var uriParser = new UriParser.UriParser();
            foreach (var x in testCases)
            {
                uriParser.Parse(x.UriString);
                string error = "";
                if ( x.Scheme != uriParser.Scheme ) error += "Scheme ";
                if ( x.User != uriParser.User ) error += "User ";
                if ( x.Password != uriParser.Password ) error += "Password ";
                if ( x.Host != uriParser.Host ) error += "Host ";
                if ( x.Port != uriParser.Port ) error += "Port ";
                if ( x.Path != uriParser.Path ) error += "Path ";
                if ( x.Query != uriParser.Query ) error += "Query ";
                if ( x.Fragment != uriParser.Fragment) error += "Fragment ";
                // uriParser.Error;
                if (error != "")
                {
                    Console.WriteLine("Error={0} Test case: {1}", error, x.ToString());
                    Console.WriteLine("Actual: UriString={0} Scheme={1} User={2} Password={3} Host={4) Port={5} Path={6} Query={7} Fragment={8} Error={9}", x.UriString, uriParser.Scheme, uriParser.User, uriParser.Password, uriParser.Host, uriParser.Port, uriParser.Path, uriParser.Query, uriParser.Fragment, uriParser.Error);
                }
            }

        }
    }
}
