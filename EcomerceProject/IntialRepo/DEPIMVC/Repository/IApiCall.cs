using DEPIMVC.Models;

namespace DEPIMVC.Repository
{
    public interface IApiCall<T>
        where T : class
    {
         Task<List<T>> GetAll(string Url);
        Task<T> GetById(string Url);
        Task<T> Insert(string url, T model);
        Task<bool> Delete(string url, string id);
       Task<bool> Update(string url, T model);
    }
}
