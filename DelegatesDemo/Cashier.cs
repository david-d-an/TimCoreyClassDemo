using System;
using DelegatesDemo.Model;

namespace DelegatesDemo;

public class Cashier {
    private readonly ShoppingCartModel _cart;

    public Cashier() {
        _cart = new ShoppingCartModel();
        PopulateCartWithDemoData();
    }

    public void RunTotal() {
        var grandTotal = _cart.GenerateTotal(
            AlertSubTotal,
            (products, subTotal) => subTotal - (products.Count * 2),
            Console.WriteLine
        );

        Console.WriteLine($"Grand Total: {grandTotal:C2}");
    }

    private static void AlertSubTotal(decimal subTotal) {
        Console.WriteLine($"The subtotal is {subTotal:C2}");
    }

    private void PopulateCartWithDemoData() {
        _cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
        _cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
        _cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
        _cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
    }
}