﻿namespace UriParser_Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtUriString = new System.Windows.Forms.TextBox();
            this.btnUriParse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScheme = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFragment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.cmdUrlTestCases = new System.Windows.Forms.Button();
            this.lstTestCaseErrors = new System.Windows.Forms.ListBox();
            this.cmdCopyClipboard = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNid = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNss = new System.Windows.Forms.TextBox();
            this.cmdUrnTestCases = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = " URI string";
            // 
            // txtUriString
            // 
            this.txtUriString.Location = new System.Drawing.Point(76, 10);
            this.txtUriString.Name = "txtUriString";
            this.txtUriString.Size = new System.Drawing.Size(598, 20);
            this.txtUriString.TabIndex = 1;
            // 
            // btnUriParse
            // 
            this.btnUriParse.Location = new System.Drawing.Point(694, 7);
            this.btnUriParse.Margin = new System.Windows.Forms.Padding(0);
            this.btnUriParse.Name = "btnUriParse";
            this.btnUriParse.Size = new System.Drawing.Size(64, 25);
            this.btnUriParse.TabIndex = 2;
            this.btnUriParse.Text = "Parse";
            this.btnUriParse.UseVisualStyleBackColor = true;
            this.btnUriParse.Click += new System.EventHandler(this.btnUriParse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Scheme";
            // 
            // txtScheme
            // 
            this.txtScheme.Location = new System.Drawing.Point(76, 36);
            this.txtScheme.Name = "txtScheme";
            this.txtScheme.Size = new System.Drawing.Size(166, 20);
            this.txtScheme.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(325, 65);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(166, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(76, 62);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(166, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Host";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(76, 88);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(166, 20);
            this.txtHost.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Path";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(76, 114);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(415, 20);
            this.txtPath.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(262, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(325, 91);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(166, 20);
            this.txtPort.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Query";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(76, 140);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(166, 20);
            this.txtQuery.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Fragment";
            // 
            // txtFragment
            // 
            this.txtFragment.Location = new System.Drawing.Point(76, 166);
            this.txtFragment.Name = "txtFragment";
            this.txtFragment.Size = new System.Drawing.Size(166, 20);
            this.txtFragment.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Error(s)";
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(76, 192);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(598, 73);
            this.txtError.TabIndex = 1;
            // 
            // cmdUrlTestCases
            // 
            this.cmdUrlTestCases.Location = new System.Drawing.Point(16, 271);
            this.cmdUrlTestCases.Name = "cmdUrlTestCases";
            this.cmdUrlTestCases.Size = new System.Drawing.Size(91, 25);
            this.cmdUrlTestCases.TabIndex = 3;
            this.cmdUrlTestCases.Text = "Run URL test cases";
            this.cmdUrlTestCases.UseVisualStyleBackColor = true;
            this.cmdUrlTestCases.Click += new System.EventHandler(this.cmdUrlTestCases_Click);
            // 
            // lstTestCaseErrors
            // 
            this.lstTestCaseErrors.FormattingEnabled = true;
            this.lstTestCaseErrors.HorizontalScrollbar = true;
            this.lstTestCaseErrors.Location = new System.Drawing.Point(16, 302);
            this.lstTestCaseErrors.Name = "lstTestCaseErrors";
            this.lstTestCaseErrors.Size = new System.Drawing.Size(779, 342);
            this.lstTestCaseErrors.TabIndex = 4;
            // 
            // cmdCopyClipboard
            // 
            this.cmdCopyClipboard.Location = new System.Drawing.Point(694, 273);
            this.cmdCopyClipboard.Name = "cmdCopyClipboard";
            this.cmdCopyClipboard.Size = new System.Drawing.Size(100, 23);
            this.cmdCopyClipboard.TabIndex = 5;
            this.cmdCopyClipboard.Text = "Copy to clipboard";
            this.cmdCopyClipboard.UseVisualStyleBackColor = true;
            this.cmdCopyClipboard.Click += new System.EventHandler(this.cmdCopyClipboard_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(539, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "NID";
            // 
            // txtNid
            // 
            this.txtNid.Location = new System.Drawing.Point(602, 39);
            this.txtNid.Name = "txtNid";
            this.txtNid.Size = new System.Drawing.Size(213, 20);
            this.txtNid.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(539, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "NSS";
            // 
            // txtNss
            // 
            this.txtNss.Location = new System.Drawing.Point(602, 69);
            this.txtNss.Name = "txtNss";
            this.txtNss.Size = new System.Drawing.Size(213, 20);
            this.txtNss.TabIndex = 1;
            // 
            // cmdUrnTestCases
            // 
            this.cmdUrnTestCases.Location = new System.Drawing.Point(122, 271);
            this.cmdUrnTestCases.Name = "cmdUrnTestCases";
            this.cmdUrnTestCases.Size = new System.Drawing.Size(91, 25);
            this.cmdUrnTestCases.TabIndex = 3;
            this.cmdUrnTestCases.Text = "Run URN test cases";
            this.cmdUrnTestCases.UseVisualStyleBackColor = true;
            this.cmdUrnTestCases.Click += new System.EventHandler(this.cmdUrnTestCases_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 656);
            this.Controls.Add(this.cmdCopyClipboard);
            this.Controls.Add(this.lstTestCaseErrors);
            this.Controls.Add(this.cmdUrnTestCases);
            this.Controls.Add(this.cmdUrlTestCases);
            this.Controls.Add(this.btnUriParse);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFragment);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNss);
            this.Controls.Add(this.txtNid);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScheme);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUriString);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "UriParser Test Harness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUriString;
        private System.Windows.Forms.Button btnUriParse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtScheme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFragment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Button cmdUrlTestCases;
        private System.Windows.Forms.ListBox lstTestCaseErrors;
        private System.Windows.Forms.Button cmdCopyClipboard;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNid;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNss;
        private System.Windows.Forms.Button cmdUrnTestCases;
    }
}

