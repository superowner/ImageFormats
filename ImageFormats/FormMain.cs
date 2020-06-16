﻿using Mechanika.ImageFormats.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

/*
 
This is a test application that tests the ImageFormats class library
included with this project. Refer to the individual source code
files for each image type for more information.

Copyright 2013-2020 by Warren Galyen
https://www.mechanikadesign.com

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

namespace ImageViewer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Text = Application.ProductName;
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0) return;
            OpenFile(files[0]);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog();
            openDlg.DefaultExt = ".*";
            openDlg.CheckFileExists = true;
            openDlg.Title = Resources.openDlgTitle;
            openDlg.Filter = "All Files (*.*)|*.*";
            openDlg.FilterIndex = 1;
            if (openDlg.ShowDialog() == DialogResult.Cancel) return;
            OpenFile(openDlg.FileName);
        }

        private void OpenFile(string fileName)
        {
            try
            {
                Bitmap bmp = null;
                bmp = MechanikaDesign.ImageFormats.Picture.Load(fileName);

                if (bmp == null)
                {
                    //try loading the file natively...
                    try { bmp = (Bitmap)Bitmap.FromFile(fileName); }
                    catch (Exception e) { Debug.WriteLine(e.Message); }
                }

                if (bmp == null)
                    throw new ApplicationException(Resources.errorLoadFailed);

                pictureBox1.Image = bmp;
                pictureBox1.Size = bmp.Size;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
