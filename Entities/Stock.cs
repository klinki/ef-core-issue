namespace ef_tpc.Entities;

public class Stock : Investment
{
    public override string InvestmentType => nameof(Stock);

    public virtual ICollection<Dividend> Dividends { get; set; }
}
