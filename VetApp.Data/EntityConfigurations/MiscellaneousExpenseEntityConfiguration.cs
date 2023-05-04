using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Finances.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class MiscellaneousExpenseEntityConfiguration : IEntityTypeConfiguration<MiscellaneousExpense>
    {
        public void Configure(EntityTypeBuilder<MiscellaneousExpense> builder)
        {
            builder.ToTable("miscellaneous_expenses");

            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.ExpenseType);
        }
    }
}