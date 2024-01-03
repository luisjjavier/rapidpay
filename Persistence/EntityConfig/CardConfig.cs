using Core.Cards;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfig
{
    /// <summary>
    /// Represents the entity configuration for the <see cref="Card"/> entity.
    /// </summary>
    public class CardConfig : RapidPayBaseEntityConfig<Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardConfig"/> class.
        /// </summary>
        public CardConfig() : base("Cards")
        {

        }

        /// <summary>
        /// Configures the entity using the specified builder.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);

            // Create an index on the Id property.
            builder.HasIndex(x => x.Id);

            // Create a unique index on the CardNumber property.
            builder.HasIndex(card => card.CardNumber).IsUnique();

            // Set the maximum length of the CardNumber property to 15 characters.
            builder.Property(card => card.CardNumber).HasMaxLength(15);

            // Configure the conversion between CardStatus enum and its string representation.
            builder
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (CardStatus)Enum.Parse(typeof(CardStatus), v));
        }
    }

}
