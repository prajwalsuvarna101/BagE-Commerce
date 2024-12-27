using Bag_E_Commerce.Models;

public class PaymentRequest
{
    public PaymentMethod PaymentMethod { get; set; }
    public string ShippingAddress {get;set;}
}