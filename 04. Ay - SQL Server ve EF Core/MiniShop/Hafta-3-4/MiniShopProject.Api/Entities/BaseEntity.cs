namespace MiniShopProject.Api.Entities;

// Tüm tabloların ortak kolonları. Id burada yok çünkü OrderItem'ın anahtarı composite (OrderId + ProductId)
public abstract class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }   // Hiç güncellenmemişse null
    public bool IsDeleted { get; set; }
}
