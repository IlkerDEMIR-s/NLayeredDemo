using FluentValidation;
using Northwind.Business.Abstract;
using Northwind.Business.Utilities;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal; //Dependency Injection

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            var validator = new ProductValidator();
            var context = new ValidationContext<Product>(product);
            ValidationTool.Validate(validator, context);
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            //Business Code
            return _productDal.GetAll();
        }

        public List<Product> GetByCategory(int CategoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == CategoryId);
        }

        public List<Product> GetByProductName(string searchKey)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(searchKey.ToLower()));

        }

        public void Update(Product product)
        {
            try
            {
                _productDal.Update(product);
            }
            catch
            {
                throw new Exception("Delete failed!");
            }
            
        }
    }
}
