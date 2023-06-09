﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Student_s_diary.Properties;

namespace Student_s_diary
{
    public partial class Main : Form
    {
        


        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        public bool IsMaximize 
        { 
            get
            {
                return Settings.Default.IsMaximize;
                
            }
            set
            {
                Settings.Default.IsMaximize = value;
            }


        }


        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();

            if (IsMaximize)
            {
                WindowState = FormWindowState.Maximized;
            }

        } 

        private void RefreshDiary()
        {
            var students =_fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;
        }

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Number";
            dgvDiary.Columns[1].HeaderText = "Name";
            dgvDiary.Columns[2].HeaderText = "Surname";
            dgvDiary.Columns[3].HeaderText = "Comments";
            dgvDiary.Columns[4].HeaderText = "Math";
            dgvDiary.Columns[5].HeaderText = "Technology";
            dgvDiary.Columns[6].HeaderText = "Physics";
            dgvDiary.Columns[7].HeaderText = "Spanish";
            dgvDiary.Columns[8].HeaderText = "English";

        }


        private void button1_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
  
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
         RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please click the student you want to edit");
                return;
            }
            var addEditStudent = new AddEditStudent(
                Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please click the student you want to DELETE");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete = MessageBox.Show($"Are you sure, to Delete the student?{(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}", "DELETE STUDENT", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id ==id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
           
                IsMaximize = true;
            else
                IsMaximize = false;

            Settings.Default.Save();
        }
    }
}
