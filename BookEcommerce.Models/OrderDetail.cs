using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookEcommerce.Models;

public class OrderDetail
{
    public int Id { get; set; }

    [Required]
    public int OrderHeaderId { get; set; }
    [ValidateNever]
    public OrderHeader OrderHeader { get; set; }

    [Required]
    public int ProductId { get; set; }
    [ValidateNever]
    public Product Product { get; set; }

    public int Count { get; set; }
    public double Price { get; set; }
}