using Core.Cards;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfig
{
    public class CardConfig: RapidPayBaseEntityConfig<Card>
    {
        public CardConfig() : base("Cards")
        {

        }

        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Id);
            builder.HasIndex(card => card.CardNumber).IsUnique();
            builder.Property(card => card.CardNumber).HasMaxLength(15);
        }
    }
}
