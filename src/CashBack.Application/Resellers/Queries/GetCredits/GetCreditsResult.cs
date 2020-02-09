namespace CashBack.Application.Resellers.Queries.GetAccumulatedCashback
{
    public class GetCreditsResult
    {
        public GetCreditsResult()
        {
        }

        public GetCreditsResult(decimal credit)
        {
            Credit = credit;
        }

        public decimal Credit { get; set; }
    }
}