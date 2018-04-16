using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevComponents.DotNetBar;

namespace Data
{
    class TracNghiem
    {
        public static IList<TEACHER> LayDanhSachGiaoVien()
        {
            IList<TEACHER> rs;
            using(var httn = new DBTracNghiemDataContext())
            {
                rs = httn.TEACHERs.ToList();
            }
            return rs;
        }
        public static IList<TYPE> LayDanhSachLoaiTaiKhoan()
        {
            IList<TYPE> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.TYPEs.ToList();
            }
            return rs;
        }
        public static IList<SUBTRACT> LayDanhSachMonHoc()
        {
            IList<SUBTRACT> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.SUBTRACTs.ToList();
            }
            return rs;
        }
        public static IList<QUIZ> LayDanhSachCauHoi()
        {
            IList<QUIZ> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.QUIZs.ToList();
            }
            return rs;
        }
        public static IList<LEVELQUIZ> LayDanhSachDoKho()
        {
            IList<LEVELQUIZ> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.LEVELQUIZs.ToList();
            }
            return rs;
        }
        public static IList<COMPETION> LayDanhSachKyThi()
        {
            IList<COMPETION> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                rs = httn.COMPETIONs.ToList();
            }
            return rs;
        }
        public static IList<GRADE> LayDanhSachKhoi()
        {
            IList<GRADE> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.GRADEs.ToList();
            }
            return rs;
        }
        public static IList<STUDENT> LayDanhSachHocSinh()
        {
            IList<STUDENT> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.DeferredLoadingEnabled = false;
                rs = httn.STUDENTs.ToList();
            }
            return rs;
        }
        public static IList<TEST> LayDanhSachDeThi()
        {
            IList<TEST> rs;
            using (var httn = new DBTracNghiemDataContext())
            {
                rs = httn.TESTs.ToList();
            }
            return rs;
        }

        public static bool ThemCauHoi(QUIZ qNew)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.QUIZs.InsertOnSubmit(qNew);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool SuaCauHoi(QUIZ qEdit)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var s = httn.QUIZs.Where(t => (t.id == qEdit.id && t.idSubtract == qEdit.idSubtract)).Select(t => t).SingleOrDefault();
                if (s != null){
                    s.quizz = qEdit.quizz;
                    s.a = qEdit.a;
                    s.b = qEdit.b;
                    s.c = qEdit.c;
                    s.d = qEdit.d;
                    s.answer = qEdit.answer;
                    s.rateHard = qEdit.rateHard;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool XoaCauHoi(int id, string maMon)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.QUIZs.Where(s => (s.id == id && s.idSubtract.Trim() == maMon)).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    httn.QUIZs.DeleteOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }
        
        public static bool ThemKyThiThat(COMPETION cNew)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.COMPETIONs.InsertOnSubmit(cNew);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool XoaKyThiThat(int code, string subtract)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.COMPETIONs.Where(s => (s.id == code && s.subtractID == subtract)).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    httn.COMPETIONs.DeleteOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool SuaKyThiThat(COMPETION cEdit)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.COMPETIONs.Where(s => (s.id == cEdit.id && s.subtractID == cEdit.subtractID)).SingleOrDefault();
                if (rs != null)
                {
                    rs.testTimeToLive = cEdit.testTimeToLive;
                    rs.testDateBegin = cEdit.testDateBegin;
                    rs.gradeid = cEdit.gradeid;
                    rs.idtest = cEdit.idtest;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool DoiMatKhauHocSinh(int ID, string pass)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.STUDENTs.Where(s => s.id == ID).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    rs.studentPassword = pass;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                    rt = false;
            }
            return rt;
        }

        public static bool DoiMatKhauGiaoVien(int ID, string pass)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.TEACHERs.Where(s => s.ID == ID).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    rs.teacherPassword = pass;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                    rt = false;
            }
            return rt;
        }
        public static bool SuaThongTinHocSinh(int id, string hoten, string email, DateTime db)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.STUDENTs.Where(s => s.id == id).Select(s => s).SingleOrDefault();
                if(rs != null)
                {
                    rs.studentFullname = hoten;
                    rs.studentEmail = email;
                    rs.studentBirthday = db;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static IList<TEST_DETAIL> LayDeThi()
        {
            IList<TEST_DETAIL> rt;
            using(var httn = new DBTracNghiemDataContext())
            {
                rt = httn.TEST_DETAILs.ToList();
            }
            return rt;
        }

        public static bool GhiBangTam(RESULT_TEMP t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.RESULT_TEMPs.Where(s =>(s.idCompe == t.idCompe && s.idStudent == t.idStudent && s.idSubtract == t.idSubtract && s.idQuiz == t.idQuiz)).Select(s=> s).SingleOrDefault();
                if (rs != null)
                {
                    //đã tồn tại thì sửa
                    rs.answer = t.answer;
                    rs.truefalse = t.truefalse;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    //chưa tồn tại thì thêm
                    httn.RESULT_TEMPs.InsertOnSubmit(t);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
            }
            return rt;
        }

        public static int SoCauTraLoiDung(int ids, int idc)
        {
            int rt = 0;
            using(var httn = new DBTracNghiemDataContext())
            {
                rt = httn.RESULT_TEMPs.Where(s => (s.idCompe == idc && s.idStudent == ids && s.truefalse == true)).Count();
            }
            return rt;
        }

        public static bool XoaBangTam(int idc, int ids, string idsub)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.RESULT_TEMPs.Where(s => (s.idCompe == idc && s.idStudent == ids && s.idSubtract == idsub)).Select(s => s);
                if (rs != null)
                {
                    httn.RESULT_TEMPs.DeleteAllOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
                
            }
            return rt;
        }

        public static IList<RESULT> LayDanhSachKetQua()
        {
            IList<RESULT> rt;
            using(var httn = new DBTracNghiemDataContext())
            {
                rt = httn.RESULTs.ToList();
            }
            return rt;
        }
        public static bool GhiKetQuaLai(RESULT t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.RESULTs.Where(s => s.idCom == t.idCom && s.idStudent == t.idStudent && s.idSubtract == t.idSubtract).SingleOrDefault();
                if (rs != null)
                {
                    rs.score = t.score;
                }
                else
                {
                    httn.RESULTs.InsertOnSubmit(t);
                }
                try
                {
                    httn.SubmitChanges();
                }
                catch
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool ThemGuest(GUEST t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                httn.GUESTs.InsertOnSubmit(t);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static IList<GUEST> LayDanhSachGuest()
        {
            IList<GUEST> rt;
            using (var httn = new DBTracNghiemDataContext())
            {
                rt = httn.GUESTs.ToList();
            }
            return rt;
        }
        public static IList<DEMO_TEST> LayDanhSachDeThiThu()
        {
            IList<DEMO_TEST> rt;
            using (var httn = new DBTracNghiemDataContext())
            {
                rt = httn.DEMO_TESTs.ToList();
            }
            return rt;
        }
        public static IList<DEMO_TEST_DETAIL> LayDanhSachCauHoiTrongDeThiThu(string mamon, int made)
        {
            IList<DEMO_TEST_DETAIL> rt;
            using(var httn = new DBTracNghiemDataContext())
            {
                rt = httn.DEMO_TEST_DETAILs.Where(s => s.demoIdTest == made & s.subtractID.Trim() == mamon.Trim()).ToList();
            }
            return rt;
        }

        public static bool GhiBangTamThiThu(DEMO_RESULT_TEMP t)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.DEMO_RESULT_TEMPs.Where(s => (s.stt == t.stt && s.idQuiz==t.idQuiz)).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    //đã tồn tại thì sửa
                    rs.answer = t.answer;
                    rs.truefalse = t.truefalse;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    //chưa tồn tại thì thêm
                    httn.DEMO_RESULT_TEMPs.InsertOnSubmit(t);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
            }
            return rt;
        }
        public static int SoCauTraLoiDungThiThu()
        {
            int rt = 0;
            using (var httn = new DBTracNghiemDataContext())
            {
                rt = httn.DEMO_RESULT_TEMPs.Where(s => (s.truefalse == true)).Count();
            }
            return rt;
        }
        public static bool XoaBangTamThiThu()
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.DEMO_RESULT_TEMPs.Select(s => s);
                if (rs != null)
                {
                    httn.DEMO_RESULT_TEMPs.DeleteAllOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }

            }
            return rt;
        }
        public static IList<DEMO_COMPETION> LayDanhSachKyThiThu()
        {
            IList<DEMO_COMPETION> rt;
            using(var httn = new DBTracNghiemDataContext())
            {
                rt = httn.DEMO_COMPETIONs.ToList();
            }
            return rt;
        }

        public static bool ThemKyThiThu(DEMO_COMPETION t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                httn.DEMO_COMPETIONs.InsertOnSubmit(t);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool XoaKyThiThu(int code, string subtract)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.DEMO_COMPETIONs.Where(s => (s.demoCompID ==code &&s.subtractID == subtract)).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    httn.DEMO_COMPETIONs.DeleteOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool SuaKyThiThu(DEMO_COMPETION t)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.DEMO_COMPETIONs.Where(s => (s.demoCompID == t.demoCompID && s.subtractID == t.subtractID)).SingleOrDefault();
                if (rs != null)
                {
                    rs.expDate = t.expDate;
                    rs.gradeid = t.gradeid;
                    rs.iddemotest = t.iddemotest;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool XoaBoDeThi(TEST t)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                //lấy 1 test_detail
                var rs = httn.TEST_DETAILs.Where(s => (s.testID == t.id && s.subtractID == t.subtractID));

                if (rs != null)
                {
                    httn.TEST_DETAILs.DeleteAllOnSubmit(rs);
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool XoaDeThi(TEST t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.TESTs.Where(s => s.id == t.id && s.subtractID == t.subtractID);
                httn.TESTs.DeleteAllOnSubmit(rs);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }

        public static bool ThemDeThi(TEST t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                httn.TESTs.InsertOnSubmit(t);
                try
                {
                    httn.SubmitChanges();
                }
                catch(Exception){
                    rt = false;
                }
            }
            return rt;
        }

        public static bool ThemChiTietDeThi(TEST_DETAIL td)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                httn.TEST_DETAILs.InsertOnSubmit(td);
                try
                {
                    httn.SubmitChanges();
                }
                catch (Exception)
                {
                    rt = false;
                }
            }
            return rt;
        }
       public static bool ThemGiaoVien(TEACHER t)
        {
            bool rt = true;
            using(var httn = new DBTracNghiemDataContext())
            {
                httn.TEACHERs.InsertOnSubmit(t);
                try
                {
                    httn.SubmitChanges();
                }
                catch
                {
                    rt = false;
                }
            }
            return rt;
        }
        public static bool SuaThongTinGiaoVien(int id, string ten, string mail, DateTime t)
        {
            bool rt = true;
            using (var httn = new DBTracNghiemDataContext())
            {
                var rs = httn.TEACHERs.Where(s => s.ID == id).Select(s => s).SingleOrDefault();
                if (rs != null)
                {
                    rs.teacherFullname = ten;
                    rs.teacherEmail = mail;
                    rs.teacherBirthday = t;
                    try
                    {
                        httn.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
            }
            return rt;
        }
    }
}
