namespace ef_tpc.Entities;

public abstract class InvestmentYield
{
    public int Id { get; set; }

    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Date { get; set; }

    public Investment Investment { get; set; }
    public int InvestmentId { get; set; }
}
