using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CustomerOperations customerOperation = new CustomerOperations();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtClientName.Text,
                CustomerSurame = txtClientSurname.Text,
                CustomerCity = txtClientCity.Text,
                CustomerBalance = decimal.Parse(txtClientBalance.Text),
                CustomerShoppingCount = int.Parse(txtClientShopping.Text)
            };

            customerOperation.AddCustomer(customer);
            MessageBox.Show("MÜşteri ekleme işlemi başarılı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperation.GetAllCustomer();
            dataGridView1.DataSource = customers;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string CustomerId = txtClientId.Text;
            customerOperation.DeleteCustomer(CustomerId);
            MessageBox.Show("Müşteri başarıyla silindi!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string CustomerId = txtClientId.Text;
            var updatedCustomer = new Customer
            {
                CustomerId = CustomerId,
                CustomerName = txtClientName.Text,
                CustomerSurame = txtClientSurname.Text,
                CustomerCity = txtClientCity.Text,
                CustomerBalance = decimal.Parse(txtClientBalance.Text),
                CustomerShoppingCount = int.Parse(txtClientShopping.Text)
            };

            customerOperation.UpdateCustomer(updatedCustomer);
            MessageBox.Show("Müşteri güncelleme işlemi başarılı!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string customerId = txtClientId.Text;
            Customer customer = customerOperation.GetCustomerById(customerId);            
            dataGridView1.DataSource = new List<Customer> { customer };
        }
    }
}
