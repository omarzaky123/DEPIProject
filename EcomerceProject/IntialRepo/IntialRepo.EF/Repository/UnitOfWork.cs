using DEPI.Core.Models;
using IntialRepo.Core.IRepositorys;
using IntialRepo.Core.Models;
using IntialRepo.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntialRepo.EF.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MyContext _myContext;
        public IBaseRepository<Author> AuthorGeneralRepository { get; private set; }
        public IBaseRepository<Product> ProductGeneralRepository { get; private set; }
        public IBaseRepository<Catigory> CatigoryGeneralRepository { get; private set; }
        public IBaseRepository<Vartion> VartionGeneralRepository { get; private set; }
        public IBaseRepository<ProductVartion> ProductVartionGeneralRepository { get; private set; }
        public IBaseRepository<ShoppingCart> ShoppingCartGeneralRepository { get; private set; }
        public IBaseRepository<CartItem> CartItemGeneralRepository { get; private set; }
        public IBaseRepository<Order> OrderGeneralRepository { get; private set; }
        public IBaseRepository<OrderItem> OrderItemGeneralRepository { get; private set; }
        public IBaseRepository<Guset> GusetGeneralRepository { get; private set; }
        public IBaseRepository<ProductImage> ProductImageRepository { get; private set; }
        public IBookRepository BookGeneralRepository { get; private set; }

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;

            AuthorGeneralRepository = new BaseRepository<Author>(_myContext);
            BookGeneralRepository = new BookRepository(_myContext);
            ProductGeneralRepository=new BaseRepository<Product>(_myContext);
            CatigoryGeneralRepository = new BaseRepository<Catigory>(_myContext);
            ProductVartionGeneralRepository = new BaseRepository<ProductVartion>(_myContext);
            ShoppingCartGeneralRepository = new BaseRepository<ShoppingCart>(_myContext);
            CartItemGeneralRepository = new BaseRepository<CartItem>(_myContext);
            GusetGeneralRepository = new BaseRepository<Guset>(_myContext);
            OrderGeneralRepository = new BaseRepository<Order>(_myContext);
            OrderItemGeneralRepository = new BaseRepository<OrderItem>(_myContext);
            VartionGeneralRepository = new BaseRepository<Vartion>(_myContext);
            ProductImageRepository = new BaseRepository<ProductImage>(_myContext);
        }

        public int Compelete()
        {
            return _myContext.SaveChanges();
        }

        public void Dispose()
        {
             _myContext.Dispose();
        }
    }
}
