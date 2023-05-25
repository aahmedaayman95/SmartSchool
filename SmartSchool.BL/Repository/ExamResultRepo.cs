using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.Migrations;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class ExamResultRepo:IExamResultRepo
    {
        public SmartSchoolContext Db { get; }
        public ExamResultRepo(SmartSchoolContext db)
        {
            Db = db;

        }
        public IEnumerable<ExamResultVM> getResultsByClassRoom(int classid)
        {


            var x = from e in Db.ExamResults.Include(s => s.Subject)
                    join s in Db.Students
                    on e.StudentId equals s.Id
                    join c in Db.ClassRooms
                    on s.ClassRoomID equals c.Id
                    //join g in Db.GradeYears
                    //on c.gradeYearId equals g.Id
                    where c.Id == classid
                    select new ExamResultVM()
                    {
                        Id=e.Id,
                        StudentId = s.Id,
                        SubjectId = e.SubjectId,
                        FirstTermGrade = e.FirstTermGrade,
                        SecondTermGrade = e.SecondTermGrade,
                        Total = e.Total,
                        SubjectName = e.Subject.Name,
                        StudentName = s.StudentFirstName,
                        ClassRoomName = c.Name,
                        Passed=e.Passed

                    };

            return x;


        }

        public void generateStudentsGrades()
        {

          if(Db.ExamResults.ToList().Count== 0)
            {

                var x = from s in Db.Students
                        join c in Db.ClassRooms
                        on s.ClassRoomID equals c.Id
                        join g in Db.GradeYears
                        on c.gradeYearId equals g.Id
                        join b in Db.Subjects
                        on g.Id equals b.GradeYearId
                        select new ExamResult()
                        {
                            StudentId = s.Id,
                            SubjectId = b.Id,
                            FirstTermGrade = 0,
                            SecondTermGrade = 0,
                            Total = 0,
                            Passed=false

                        };


                Db.ExamResults.AddRange(x);
                Db.SaveChanges();

            }


            //var students = Db.Students.ToList();
            ////var studentsAttendances = Db.StudAttendances.ToList();
            //var examResult = Db.ExamResults.ToList();

            ////check condition for date
            //if (examResult.Count == 0)
            //{
            //    foreach (var stud in students)
            //    {

            //        var gradeYearSubjects = Db.Students.Where(s=>s.ClassRoomID==stud.ClassRoomID).Select(s => s.ClassRoom.gradeYear.Subjects).ToList();


            //        foreach (var sub in gradeYearSubjects)
            //        {


            //            ExamResult exam = new ExamResult()
            //            {
            //               StudentId=stud.Id,
            //               SubjectId=sub.id
            //            };
            //            Db.ExamResults.Add(exam);
            //        }

            //    }

            //    Db.SaveChanges();
            //}

        }
        public void Edit(List<ExamResultVM> Result)
        {

            //var x= Db.ExamResults

            foreach (var e in Result)
            {

                var result = Db.ExamResults.Where(x=>x.Id==e.Id).Include(e => e.Subject).FirstOrDefault();

                result.StudentId = e.StudentId;
                result.SubjectId = e.SubjectId;
                result.FirstTermGrade = e.FirstTermGrade;
                result.SecondTermGrade = e.SecondTermGrade;
                result.Total = e.FirstTermGrade + e.SecondTermGrade;
                if (result.Total >= (result.Subject.TotalMark) / 2)
                {
                    result.Passed= true;
                }

                Db.Update(result);



            }

            Db.SaveChanges();   


        }

        public void upgradeStudent()
        {
            var students = Db.Students.ToList();
            bool allPassed = true;

            foreach (var student in students)
            {
                var studentResults = Db.ExamResults.Include(s => s.Subject).Where(r => r.StudentId == student.Id);

                foreach (var r in studentResults)
                {

                    if (!r.Passed)
                    {
                        allPassed= false;
                    }

                }


                if (allPassed)
                {
                    var studentClass = Db.ClassRooms.Where(c=>c.Id==student.ClassRoomID).FirstOrDefault();

                    var studentClassSplit= studentClass.Name.Split('/');

                    int gradeYear= int.Parse(studentClassSplit[0])+1;

                    var classNumber =  studentClassSplit[1];

                    var studentClassUpgrade=gradeYear.ToString()+"/"+classNumber;


                    var studentNewClass = Db.ClassRooms.Where(c => c.Name == studentClassUpgrade).FirstOrDefault();

                    if(studentNewClass != null)
                    {

                        student.ClassRoomID = studentNewClass.Id;
                    }


                }

                Db.SaveChanges();

                allPassed = true;

            }


            var sourceData = Db.ExamResults.ToList();

            Db.Database.ExecuteSql($"insert into PreviousExamResults select FirstTermGrade,SecondTermGrade,Total,Passed,StudentId,SubjectId from ExamResults");
            Db.ExamResults.RemoveRange(sourceData);

            Db.SaveChanges();


            //var sourceData = Db.ExamResults.ToList();


            //foreach (var s in sourceData)
            //{
            //    var previousExamResult = new PreviousExamResult()
            //    {
            //        StudentId = s.StudentId,
            //        FirstTermGrade = s.FirstTermGrade,
            //        Passed = s.Passed,
            //        SecondTermGrade = s.SecondTermGrade,
            //        SubjectId = s.SubjectId,
            //        Total = s.Total

            //    };
            //    Db.PreviousExamResults.Add(previousExamResult);
            //}
            //Db.ExamResults.RemoveRange(sourceData);
            //Db.SaveChanges();



        }

        #region Parent and Student Role
        public List<ExamResultVM> getStudentExamResults(string studentId, int gradeYearId)
        {
            var grades = Db.PreviousExamResults.Where(e => e.StudentId == studentId && e.Subject.GradeYearId == gradeYearId).Include(s => s.Subject).Select(x => new ExamResultVM()
            {
                FirstTermGrade = x.FirstTermGrade,
                SecondTermGrade = x.SecondTermGrade,
                Passed = x.Passed,
                SubjectId = x.SubjectId,
                StudentId = x.StudentId,
                Total = x.Total,

            }).ToList();
            return grades;
        }

        #endregion


    }
}
