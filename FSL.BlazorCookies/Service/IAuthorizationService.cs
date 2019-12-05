using FSL.BlazorCookies.Models;
using System.Threading.Tasks;

namespace FSL.BlazorCookies.Service
{
    public interface IAuthorizationService
    {
        Task<BaseResult<IUser>> AuthorizeAsync(
            LoginUser loginUser);
    }
}
