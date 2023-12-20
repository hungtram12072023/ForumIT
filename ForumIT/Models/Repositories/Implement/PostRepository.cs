using ForumIT.Models;
using ForumIT.Models.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ForumIT.Models.Repositories.Implement
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumITContext db;
        private readonly IWebHostEnvironment environment;

        public PostRepository(ForumITContext db, IWebHostEnvironment environment)
        {
            this.db = db;
            this.environment = environment;
        }

        public async Task<List<TblBaiViet>> GetAllAsync()
        {
            return await db.TblBaiViets.ToListAsync();
        }

        public Task<TblBaiViet?> GetByIdAsync(int id)
        {
            return db.TblBaiViets.FirstOrDefaultAsync(p => p.IdBaiViet == id);
        }

        public async Task<TblBaiViet> CreateAsync(TblBaiViet post)
        {
            await db.TblBaiViets.AddAsync(post);
            await db.SaveChangesAsync();
            return post;
        }


        public async Task<bool> UpdateAsync(TblBaiViet post)
        {
            db.TblBaiViets.Update(post);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<TblBaiViet?> DeleteAsync(int id)
        {
            var postExisting = db.TblBaiViets.FirstOrDefault(p => p.IdBaiViet == id);
            if (postExisting == null) { return null; }
            db.TblBaiViets.Remove(postExisting);
            await db.SaveChangesAsync();
            return postExisting;
        }


        public async Task<List<TblBaiViet>> SearchByName(string name)
        {
            return await db.TblBaiViets.Where(p => EF.Functions.Like(p.NoiDung, $"%{name}%")).ToListAsync();
        }

        public async Task<object> UpdatePost(int id, string title, string Description, int loadd, DateTime PostDate, IFormFile imageFile)
        {
            var post = await db.TblBaiViets.FindAsync(id);
            if (post == null)
            {
                return new
                {
                    success = false,
                    message = "Product not found"
                };
            }
            post.Ngaydang = PostDate;
            post.NoiDung = Description;
            post.Title = title;
            post.IdBaiViet = id;

            if (imageFile != null)
            {
                var imagePath = Path.Combine(environment.WebRootPath, "Uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(imagePath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                post.Img = fileName;
            }

            await db.SaveChangesAsync();

            return new
            {
                success = true,
                message = "Update Successfully!",
                post
            };


        }

    }
}

