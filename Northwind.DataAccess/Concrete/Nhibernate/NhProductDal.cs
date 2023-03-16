using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.Nhibernate
{
    public class NhProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> list = new List<Product>();

            list.Add(new Product
            {
                ProductId = 1,
                CategoryId = 1,
                ProductName = "Laptop",
                QuantityPerUnit = "one in a box",
                UnitPrice = 3000,
                UnitsInStock = 20
            });

            return list;
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }

}
