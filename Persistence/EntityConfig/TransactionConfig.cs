using Core.Transactions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfig
{
    public class TransactionConfig : RapidPayBaseEntityConfig<Transaction>
    {
        public TransactionConfig() : base("Transactions")
        {

        }

        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.TransactionStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentTransactionStatus)Enum.Parse(typeof(PaymentTransactionStatus), v));
        }
    }

}
