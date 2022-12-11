namespace ef_tpc.Entities;

public class Bond : Investment
{
    public override string InvestmentType => nameof(Bond);

    public decimal Yield { get; set; }

    public DateTime MaturityDate { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; }
}
