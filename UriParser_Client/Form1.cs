﻿using System;
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
            ClearOutput();
            var urnParser = new UriParser.UrnParser();
            urnParser.Parse(txtUriString.Text);
            if (urnParser.Error == "")
            {
                txtScheme.Text = urnParser.Scheme;
                txtNid.Text = urnParser.Nid;
                txtNss.Text = urnParser.Nss;
                txtError.Text = urnParser.Error;
            }
            else
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
        }

        private void ClearOutput()
        {
            txtScheme.Text = "";
            txtUser.Text = "";
            txtPassword.Text = "";
            txtHost.Text =  "";
            txtPort.Text =  "";
            txtPath.Text =  "";
            txtQuery.Text =  "";
            txtFragment.Text =  "";
            txtError.Text = "";
            txtNid.Text = "";
            txtNss.Text = "";

        }

        // Parse a URI into its components
        private class UriTestCase
        {
            public string UriString;
            public string Scheme;
            public string Error;
            public string Comment;

            public override string ToString()
            {
                string desc = String.Format("UriString={0} Scheme={1} Error={2} Comment={3}",
                    UriString, Scheme, Error, Comment);
                return desc;
            }
        }

        // Parse a URL into its components
        private class UrlTestCase : UriTestCase
        {
            public string User;
            public string Password;
            public string Host;
            public string Port;
            public string Path;
            public string Query;
            public string Fragment;

            public override string ToString()
            {
                string desc = String.Format("UriString={0} Scheme={1} User={2} Password={3} Host={4} Port={5} Path={6} Query={7} Fragment={8} Error={9} Comment={10}",
                    UriString, Scheme, User, Password, Host, Port, Path, Query, Fragment, Error, Comment);
                return desc;
            }
        }


        // Parse a URN into its components
        private class UrnTestCase : UriTestCase
        {
            public string Nid;
            public string Nss;

            public override string ToString()
            {
                string desc = String.Format("UriString={0} Scheme={1} Nid={2} Nss={3} Error={4} Comment={5}",
                    UriString, Scheme, Nid, Nss, Error, Comment);
                return desc;
            }
        }

        // Both error strings should be empty or both populated - can't check equality because one is a human remark, the other generated in code
        private bool IsErrorMatch( string s1, string s2 )
        {
            bool isMatch = (s1.Trim() == "" && s2.Trim() == "") || (s1.Trim() != "" && s2.Trim() != "");
            return isMatch;
        }

        private void cmdUrlTestCases_Click(object sender, EventArgs e)
        {
            lstTestCaseErrors.Items.Clear();

            var testCases = new List<UrlTestCase>();
            string line;
            StreamReader file = new StreamReader(@"url-test-cases.txt");
            int n = 0;
            while ((line = file.ReadLine()) != null)
            {
                ++n;
                // Skip header line
                if (n == 1)
                    continue;

                var fields = line.Split(new char[] { '\t' }, StringSplitOptions.None);

                var item = new UrlTestCase();
                item.UriString = fields[0].Trim();
                item.Scheme = fields[1];
                item.User = fields[2];
                item.Password = fields[3];
                item.Host = fields[4];
                item.Port = fields[5];
                item.Path = fields[6];
                item.Query = fields[7];
                item.Fragment = fields[8];
                item.Error = fields[9];
                item.Comment = fields[10];

                testCases.Add(item);
            }

            var uriParser = new UriParser.UriParser();
            for (int i = 0; i < testCases.Count; ++i)
            {
                var x = testCases[i];
                uriParser.Parse(x.UriString);

                string error = "";
                if (x.Scheme != uriParser.Scheme) error += "Scheme.";
                if (x.User != uriParser.User) error += "User.";
                if (x.Password != uriParser.Password) error += "Password.";
                if (x.Host != uriParser.Host) error += "Host.";
                if (x.Port != uriParser.Port) error += "Port.";
                if (x.Path != uriParser.Path) error += "Path.";
                if (x.Query != uriParser.Query) error += "Query.";
                if (x.Fragment != uriParser.Fragment) error += "Fragment.";
                if (!IsErrorMatch(x.Error, uriParser.Error)) error += "ErrorMismatch.";  // Error mismatch
                if (error != "")
                {
                    string expected = String.Format("Error={0} Expected: {1}", error, x.ToString());
                    string actual = String.Format("\tActual: UriString={0} Scheme={1} User={2} Password={3} Host={4} Port={5} Path={6} Query={7} Fragment={8} Error={9}",
                        x.UriString, uriParser.Scheme, uriParser.User, uriParser.Password, uriParser.Host, uriParser.Port, uriParser.Path, uriParser.Query, uriParser.Fragment, uriParser.Error);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);
                    lstTestCaseErrors.Items.Add(expected);
                    lstTestCaseErrors.Items.Add(actual);
                }
            }

        }

        private void cmdUrnTestCases_Click(object sender, EventArgs e)
        {
            lstTestCaseErrors.Items.Clear();

            var testCases = new List<UrnTestCase>();
            string line;
            StreamReader file = new StreamReader(@"urn-test-cases.txt");
            int n = 0;
            while ((line = file.ReadLine()) != null)
            {
                ++n;
                // Skip header line
                if (n == 1)
                    continue;

                var fields = line.Split(new char[] { '\t' }, StringSplitOptions.None);

                var item = new UrnTestCase();
                item.UriString = fields[0].Trim();
                item.Scheme = fields[1];
                item.Nid = fields[2];
                item.Nss = fields[3];
                item.Error = fields[4];
                item.Comment = fields[5];

                testCases.Add(item);
            }

            var urnParser = new UriParser.UrnParser();
            for (int i = 0; i < testCases.Count; ++i)
            {
                var x = testCases[i];
                urnParser.Parse(x.UriString);

                string error = "";
                if (x.Scheme != urnParser.Scheme) error += "Scheme.";
                if (x.Nid != urnParser.Nid) error += "NID.";
                if (x.Nss != urnParser.Nss) error += "NSS.";
                if (!IsErrorMatch(x.Error, urnParser.Error)) error += "ErrorMismatch.";  // Error mismatch
                if (error != "")
                {
                    string expected = String.Format("Error={0} Expected: {1}", error, x.ToString());
                    string actual = String.Format("\tActual: UriString={0} Scheme={1} NID={2} NSS={3} Error={4}",
                        x.UriString, urnParser.Scheme, urnParser.Nid, urnParser.Nss, urnParser.Error);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);
                    lstTestCaseErrors.Items.Add(expected);
                    lstTestCaseErrors.Items.Add(actual);
                }
            }

        }

        private void cmdCopyClipboard_Click(object sender, EventArgs e)
        {
            Utils.CopyListBox(lstTestCaseErrors, this); // copy contents to Windows clipboard
        }

    }
}
