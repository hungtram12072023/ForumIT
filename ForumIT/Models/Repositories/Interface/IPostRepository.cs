using ForumIT.Models;
using Microsoft.Extensions.Hosting;

namespace ForumIT.Models.Repositories.Interface
{
    public interface IPostRepository
    {
        Task<List<TblBaiViet>> GetAllAsync();
        Task<TblBaiViet?> GetByIdAsync(int id);
        Task<TblBaiViet> CreateAsync(TblBaiViet post);

        Task<bool> UpdateAsync(TblBaiViet post);

        Task<object> UpdatePost(int id, string title, string Description, int loadd, DateTime PostDate, IFormFile imageFile);

        Task<TblBaiViet> DeleteAsync(int id);

        Task<List<TblBaiViet>> SearchByName(string name);

    }
}
