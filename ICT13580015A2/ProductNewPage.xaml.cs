using System;
using System.Collections.Generic;
using ICT13580015A2.Models;
using Xamarin.Forms;
namespace ICT13580015A2
{
	public partial class ProductNewPage : ContentPage
	{
        Product product;

        public ProductNewPage(Product product = null)
		{
			InitializeComponent();

            this.product = product;

            titleLabel.Text = product == null ? "เพิ่มสินค้าใหม่" : "แก้ไขข้อมูลสินค้า";


			okButton.Clicked += OkButton_Clicked;
			cancelButton.Clicked += CancelButton_Clicked;

			categoryPicker.Items.Add("เสื้อ");
			categoryPicker.Items.Add("กางเกง");
			categoryPicker.Items.Add("รองเท้า");
            categoryPicker.Items.Add("ถุงเท้า");

            if (product != null)
            {
                nameEntry.Text = product.Name;
                desEntry.Text = product.Descrpition;
                categoryPicker.SelectedItem = product.Category;
                productPriceEntry.Text = product.ProductPrice.ToString();
                sellPriceEntry.Text = product.SellPrice.ToString();
                stockEntry.Text = product.Stock.ToString();
            }
		}

       
        async void OkButton_Clicked(object sender, EventArgs e)
		{
			var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการบัญทึกใช่หรือไม่", "ใช่", "ไม่ใช่");
			if (isOk)
			{
                if (product == null)
                {
                    product = new Product();
                    product.Name = nameEntry.Text;
                    product.Descrpition = desEntry.Text;
                    product.Category = categoryPicker.SelectedItem.ToString();
                    product.ProductPrice = decimal.Parse(productPriceEntry.Text);
                    product.SellPrice = decimal.Parse(sellPriceEntry.Text);
                    product.Stock = int.Parse(stockEntry.Text);
                    var id = App.DbHelper.AddProduct(product);
                    await DisplayAlert("บันทึกสำเร็จ", "รหัสสินค้าของท่านคือ #" + id, "ตกลง");
                }
                else
                {
					product.Name = nameEntry.Text;
					product.Descrpition = desEntry.Text;
					product.Category = categoryPicker.SelectedItem.ToString();
					product.ProductPrice = decimal.Parse(productPriceEntry.Text);
					product.SellPrice = decimal.Parse(sellPriceEntry.Text);
					product.Stock = int.Parse(stockEntry.Text);
                    var id = App.DbHelper.UpdateProduct(product);
					await DisplayAlert("บันทึกสำเร็จ", "แก้ไขข้อมูลสินค้าเรียบร้อย" , "ตกลง");
                }
                await Navigation.PopModalAsync();
			}
		}
		void CancelButton_Clicked(object sender, EventArgs e)
		{
            Navigation.PopModalAsync();
		}
	}
}