using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_s_diary
{
    public partial class AddEditStudent : Form
    {

        private int _studentId;

        private Student _student;
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;

            GetStudentData();
            tbtFirstName.Select();
        }

      


        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edit student's date";

                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("No student with this ID");
                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbtId.Text = _student.Id.ToString();
            tbtFirstName.Text = _student.FirstName;
            tbtLastName.Text = _student.LastName;
            tbtMath.Text = _student.Math;
            tbtPhysic.Text = _student.Physics;
            tbtTechnology.Text = _student.Technology;
            tbtSpanish.Text = _student.SpanishLang;
            tbtEnglish.Text = _student.EnglishLang;
            rtbComments.Text = _student.Comments;

        }


        private void AddEditStudent_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AssigIdToNewStudent(List<Student> students)
        {

        }


        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
            
                students.RemoveAll(x => x.Id == _studentId);
            
            else
            
                AssignIdToNewStudent(students);
                AddNewUserToList(students);


            _fileHelper.SerializeToFile(students);

           await LongProcessAsync();

                Close();
        }

       


        private async Task LongProcessAsync()
        {
         
           await  Task.Run(() =>
                {
                    Thread.Sleep(3000);
                });
           
        }



        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbtFirstName.Text,
                LastName = tbtLastName.Text,
                Comments = rtbComments.Text,
                Math = tbtMath.Text,
                Physics = tbtPhysic.Text,
                Technology = tbtTechnology.Text,
                SpanishLang = tbtSpanish.Text,
                EnglishLang = tbtEnglish.Text
            };

            students.Add(student);

        }

            private  void AssignIdToNewStudent(List<Student>students)
            {
                var studentWithHighestId = students.OrderByDescending(x => x.Id).FirstOrDefault();

                _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;
            }
     }
 }
    

