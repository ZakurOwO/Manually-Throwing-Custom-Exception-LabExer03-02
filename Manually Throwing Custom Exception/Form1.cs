using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manually_Throwing_Custom_Exception
{
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string varName) : base(varName) { }
    }

    public class StringFormatException : Exception
    {
        public StringFormatException(string varName) : base(varName) { }
    }

    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string varName) : base(varName) { }
    }

    public partial class Form1 : Form
    {
        private BindingSource showProductList;
        private int _Quantity;
        private double _SellingPrice;
        private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

        public class ProductClass
        {
            private int _Quantity;
            private double _SellingPrice;
            private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;

            public ProductClass(string ProductName, string Category, string MfgDate, string ExpDate,
            double Price, int Quantity, string Description)
            {
                this._Quantity = Quantity;
                this._SellingPrice = Price;
                this._ProductName = ProductName;
                this._Category = Category;
                this._ManufacturingDate = MfgDate;
                this._ExpirationDate = ExpDate;
                this._Description = Description;
            }

            public string productName
            {
                get
                {
                    return this._ProductName;
                }
                set
                {
                    this._ProductName = value;
                }
            }
            public string category
            {
                get

                {
                    return this._Category;
                }
                set
                {
                    this._Category = value;
                }
            }
            public string manufacturingDate
            {
                get
                {
                    return this._ManufacturingDate;
                }
                set
                {
                    this._ManufacturingDate = value;
                }
            }
            public string expirationDate
            {
                get
                {
                    return this._ExpirationDate;
                }
                set
                {
                    this._ExpirationDate = value;
                }
            }
            public string description
            {
                get
                {
                    return this._Description;
                }
                set
                {
                    this._Description = value;
                }
            }
            public int quantity
            {
                get
                {
                    return this._Quantity;
                }
                set
                {
                    this._Quantity = value;
                }
            }
            public double sellingPrice
            {
                get
                {
                    return this._SellingPrice;
                }
                set
                {
                    this._SellingPrice = value;
                }
            }
        }

        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException("Product name must only contain letters.");
                }
                return name;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Product Name");
                return string.Empty;
            }
            finally
            {
                Console.WriteLine("Product_Name validation completed.");
            }
        }

        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                {
                    throw new NumberFormatException("Quantity must be numeric.");
                }
                return Convert.ToInt32(qty);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Quantity");
                return 0;
            }
            finally
            {
                Console.WriteLine("Quantity validation completed.");
            }
        }

        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException("Selling price must be numeric.");
                }
                return Convert.ToDouble(price);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Price");
                return 0.0;
            }
            finally
            {
                Console.WriteLine("SellingPrice validation completed.");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _ManufacturingDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpirationDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellingPrice = SellingPrice(txtSellPrice.Text);

            if (!string.IsNullOrEmpty(_ProductName) && _Quantity > 0 && _SellingPrice > 0)
            {
                showProductList.Add(new ProductClass(_ProductName, _Category, _ManufacturingDate,
                _ExpirationDate, _SellingPrice, _Quantity, _Description));

                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductsCategory = { "Beverage", "Bread/Bakery", "Canned/Jarred Goods", "Dairy",
                                        "Frozen Goods", "Meat", "Personal Care", "Other" };

            foreach (string category in ListOfProductsCategory)
            {
                cbCategory.Items.Add(category);
            }
            showProductList = new BindingSource();
        }
    }
}
