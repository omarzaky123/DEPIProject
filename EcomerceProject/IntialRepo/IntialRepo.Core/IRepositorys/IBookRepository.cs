using IntialRepo.Core.Models;
using IntialRepo.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntialRepo.Core.IRepositorys
{
    //This is Book Repository Where Include  All the BaseRepository
    //and the BookRepository
    public interface IBookRepository:IBaseRepository<Book>
    {
       string SpecialFunctionOfBook();
    }
}
