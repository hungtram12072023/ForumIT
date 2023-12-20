using ForumIT.Models;

namespace ForumIT.Models.Repositories.Interface
{
    public interface IType
    {
        TblLoaiDd Add(TblLoaiDd tbldd);
        TblLoaiDd Update(TblLoaiDd tbldd);
        TblLoaiDd Delete(int IDMember);
        TblLoaiDd Get(int IDMember);
        IEnumerable<TblLoaiDd> GetAll();
    }
}
