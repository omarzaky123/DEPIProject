using IntialRepo.Core.IRepositorys;
using IntialRepo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntialRepo.EF.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        //it already register in the BaseRepository<Book> 
        public BookRepository(MyContext myContext):base(myContext) { }
        public string SpecialFunctionOfBook()
        {
            return "Ok this is From Special";
        }
    }
}
