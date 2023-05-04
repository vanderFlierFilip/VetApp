using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Finances.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class MiscellaneousExpenseTypeEntityConfiguration : IEntityTypeConfiguration<MiscellaneousExpenseType>
    {
        public void Configure(EntityTypeBuilder<MiscellaneousExpenseType> builder)
        {
            builder.ToTable("miscellaneous_expense_types");
            builder.HasKey(x => x.Id);
        }
    }
}