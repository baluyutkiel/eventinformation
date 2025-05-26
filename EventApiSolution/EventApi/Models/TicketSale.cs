namespace EventApi.Models
{
    public class TicketSale
    {
        public virtual string Id { get; set; } = default!;
        public virtual string EventId { get; set; } = default!;
        public virtual string UserId { get; set; } = default!;
        public virtual DateTime PurchaseDate { get; set; }
        public virtual int PriceInCents { get; set; }
    }
}