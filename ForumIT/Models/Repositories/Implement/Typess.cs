using ForumIT.Models;
using ForumIT.Models.Repositories.Interface;

namespace ForumIT.Models.Repositories.Implement
{
    public class Typess : IType
    {
        private readonly ForumITContext _conext;

        public Typess(ForumITContext context)
        {
            _conext = context;
        }
        public TblLoaiDd Add(TblLoaiDd tbldd)
        {
            _conext.Add(tbldd);
            _conext.SaveChanges();
            return tbldd;
        }

        public TblLoaiDd Delete(int IDMember)
        {
            TblLoaiDd tbl = _conext.TblLoaiDds.Find(IDMember);
            _conext.TblLoaiDds.Remove(tbl);
            _conext.SaveChanges();
            return tbl;
        }

        public TblLoaiDd Get(int IDMember)
        {
            return _conext.TblLoaiDds.Find(IDMember);
        }

        public IEnumerable<TblLoaiDd> GetAll()
        {
            return _conext.TblLoaiDds;
        }

        public TblLoaiDd Update(TblLoaiDd tbldd)
        {
            _conext.Update(tbldd);
            _conext.SaveChanges();
            return tbldd;
        }


    }
}
