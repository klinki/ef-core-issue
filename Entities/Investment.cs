namespace ef_tpc.Entities;

public interface IInvestment
{
}

public abstract class Investment : IInvestment
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public abstract string InvestmentType { get; }
}
