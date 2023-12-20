using ForumIT.Models.DTO;

namespace ForumIT.Models.Repositories.Interface
{
    public interface IAuRepository
    {
        Task<Status> LoginAsync(LoginDTO loginRequestDto);

        Task LogOutAsync();

        Task<Status> RegisterAsync(RegisterDTO registerRequestDto);


    }
}
