using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.DataAccess.Concrete.Nhibernate;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>(); // Dependency injection
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();

            //_productService = new ProductManager(new EfProductDal()); // Not suitable for dependency inversion
            //_categoryService = new CategoryManager(new EfCategoryDal());
        }

        private IProductService _productService;
        private ICategoryService _categoryService;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();

        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName"; // CategoryName is the name of the column in the database
            cbxCategory.ValueMember = "CategoryId"; // CategoryId is the name of the column in the database

            cbxCategoryAdd.DataSource = _categoryService.GetAll();
            cbxCategoryAdd.DisplayMember = "CategoryName";
            cbxCategoryAdd.ValueMember = "CategoryId";

            cbxCategoryUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryUpdate.DisplayMember = "CategoryName";
            cbxCategoryUpdate.ValueMember = "CategoryId";            
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                dgwProduct.DataSource = _productService.GetByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch { }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        { 

            if (!String.IsNullOrEmpty(tbxSearch.Text))
            {
            dgwProduct.DataSource = _productService.GetByProductName(tbxSearch.Text);
            }
            else
            {
                LoadProducts();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Add(new Entities.Concrete.Product
                {
                    CategoryId = Convert.ToInt32(cbxCategoryAdd.SelectedValue),
                    ProductName = tbxProductName.Text,
                    QuantityPerUnit = tbxQuantity.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text)
                });
                MessageBox.Show("Product added successfully");
                LoadProducts();

                tbxProductName.Text = "";
                tbxQuantity.Text = "";
                tbxUnitPrice.Text = "";
                tbxStock.Text = "";
        }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Entities.Concrete.Product
            {
                ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                CategoryId = Convert.ToInt32(cbxCategoryUpdate.SelectedValue),
                ProductName = tbxUpdateName.Text.ToString(),
                QuantityPerUnit = tbxUpdateQuantity.Text,
                UnitPrice = Convert.ToDecimal(tbxUpdateUnitPrice.Text),
                UnitsInStock = Convert.ToInt16(tbxUpdateStock.Text)
            });
            MessageBox.Show("Product updated successfully");
            LoadProducts();

        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                var row = dgwProduct.CurrentRow;
                tbxUpdateName.Text = row.Cells[1].Value.ToString();
                cbxCategoryUpdate.SelectedValue = row.Cells[2].Value;
                tbxUpdateUnitPrice.Text = row.Cells[3].Value.ToString();
                tbxUpdateQuantity.Text = row.Cells[4].Value.ToString();
                tbxUpdateStock.Text = row.Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgwProduct.CurrentRow != null)
            {
                try {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)

                    });
                    MessageBox.Show("Product deleted!");
                    LoadProducts();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }


        }
    } 
}
