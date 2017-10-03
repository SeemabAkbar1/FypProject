//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using System.Configuration;
//using System.Collections;
//using PUCIT.AIMRL.WebAppName.DAL;
//using PUCIT.AIMRL.WebAppName.UI.Common;
//using PUCIT.AIMRL.WebAppName.Entities;

//namespace PUCIT.AIMRL.WebAppName.MainApp.Models
//{
//    public class SamplesRepository 
//    {
//        private PRMDataService _dataService;
//        public SamplesRepository()
//        {
//        }

//        private PRMDataService DataService
//        {
//            get
//            {
//                if (_dataService == null)
//                    _dataService = new PRMDataService();

//                return _dataService;
//            }
//        }

        

//        public Object SearchSampleStudents(Int32? pStudentId, string pFirstName, string pLastName, DateTime? pDateOfBirth)
//        {
//            try
//            {
//                var StudentList = DataService.SearchSampleStudents(pStudentId, pFirstName, pLastName, pDateOfBirth);

                 
//                return (new
//                {
//                    data = new
//                    {
//                        StudentList = StudentList
//                    },
//                    success = true,
//                    error = ""
//                });
//            }
//            catch (Exception ex)
//            {
//                return (new
//                {
//                    success = false,
//                    error = "Some Error has occurred"
//                });
//            }
//        }
//        public Object SearchSampleStudentsForAuto(String prefixText)
//        {
//            try
//            {
//                var StudentList = DataService.SearchSampleStudentsForAuto(prefixText);


//                var result = (from p in StudentList
//                             select new
//                             {
//                                 TAG_VALUE = p.StudentId,
//                                 TAG_LABEL = p.FirstName + " " + p.LastName,
//                             }).ToList();

//                return result;
//            }
//            catch (Exception ex)
//            {
//                return (new
//                {
//                    success = false,
//                    error = "Some Error has occurred"
//                });
//            }
//        }
//        public Object SaveSampleStudent(SampleStudent pStudent)
//        {
//            try
//            {
//                String pUser = SessionManager.GetUserFullName();
//                DateTime dt = DateTime.UtcNow;

//                //if (pStudent.StudentId == 0)
//                //{
//                //    pStudent.CreatedOn = DateTime.UtcNow;
//                //    pStudent.CreatedBy = SessionManager.GetUserFullName();
//                //    pStudent.IsActive = true;
//                //}
//                //else
//                //{
//                //    pStudent.ModifiedOn = DateTime.UtcNow;
//                //    pStudent.ModifiedBy = SessionManager.GetUserFullName();
//                //    pStudent.IsActive = true;
//                //}

                
//                var StudentId = DataService.SaveSampleStudentLINQ(pStudent, pUser,dt);


//                //var StudentId = DataService.SaveSampleStudentSP(pStudent, pUser, dt);


//                return (new
//                {
//                    data = StudentId,
//                    success = true,
//                    error = ""
//                });
//            }
//            catch (Exception ex)
//            {
//                return (new
//                {
//                    success = false,
//                    error = "Some Error has occurred"
//                });
//            }
//        }

//        public Object DeactivateSampleStudent(int pStudentId)
//        {
//            bool rowDeleted = false;
//            try
//            {
//                String modifiedBy = SessionManager.CurrentUser.Login;
//                DateTime modifiedOn = DateTime.UtcNow;
                 
//                //rowDeleted = DataService.DeactivateSampleStudent(pStudentId, modifiedBy, modifiedOn);

//                rowDeleted = DataService.DeactivateSampleStudentSP(pStudentId, modifiedBy, modifiedOn);

//                return (new
//                { 
//                    success = true,
//                    message = "Record has been deleted successfully"
//                });
//            }
//            catch (Exception ex)
//            {
//                return (new
//                {
//                    success = false,
//                    error = "Some Error Has Occurred"
//                });
//            }
//        }

//    }

//}
