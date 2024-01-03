using Core.Transactions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfig
{
    /// <summary>
    /// Represents the entity configuration for the <see cref="Transaction"/> entity.
    /// </summary>
    public class TransactionConfig : RapidPayBaseEntityConfig<Transaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionConfig"/> class.
        /// </summary>
        public TransactionConfig() : base("Transactions")
        {

        }

        /// <summary>
        /// Configures the entity using the specified builder.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            // Configure the conversion between PaymentTransactionStatus enum and its string representation.
            builder
                .Property(e => e.TransactionStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentTransactionStatus)Enum.Parse(typeof(PaymentTransactionStatus), v));
        }
    }


}
