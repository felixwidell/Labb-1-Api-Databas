using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface IAdminRepository
    {
        Task Register(RegisterAdminDto dto);
        Task Login(LoginAdminDto loginAdmin);
    }
}
