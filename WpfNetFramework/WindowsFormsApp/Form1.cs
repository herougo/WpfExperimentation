﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void IncrementButton_Click(object sender, EventArgs e)
        {
            int.TryParse(CountTextBox.Text, out int result);
            CountTextBox.Text = (result+1).ToString();
        }
    }
}
