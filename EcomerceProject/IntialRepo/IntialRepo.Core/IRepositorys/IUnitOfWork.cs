using DEPI.Core.Models;
using IntialRepo.Core.Models;
using IntialRepo.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntialRepo.Core.IRepositorys
{
    public interface IUnitOfWork:IDisposable
    {

        IBaseRepository<Author> AuthorGeneralRepository { get; }
        IBaseRepository<ProductVartion> ProductVartionGeneralRepository { get; }
        IBookRepository BookGeneralRepository { get;}
        IBaseRepository<Product> ProductGeneralRepository { get;}
        IBaseRepository<ShoppingCart> ShoppingCartGeneralRepository { get;}
        IBaseRepository<CartItem> CartItemGeneralRepository { get;}
        IBaseRepository<Order> OrderGeneralRepository { get;}
        IBaseRepository<Vartion> VartionGeneralRepository { get;}
        IBaseRepository<Guset> GusetGeneralRepository { get;}
        IBaseRepository<OrderItem> OrderItemGeneralRepository { get;}
        IBaseRepository<Catigory> CatigoryGeneralRepository { get;}
        IBaseRepository<ProductImage> ProductImageRepository { get;}
        int Compelete();

    }
}
