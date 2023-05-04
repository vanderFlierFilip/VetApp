using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Finances.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class SupplierExpenseEntityConfiguration : IEntityTypeConfiguration<SupplierExpense>
    {
        public void Configure(EntityTypeBuilder<SupplierExpense> builder)
        {
            builder.ToTable("supplier_expenses");
            builder.HasKey(t => t.Id);
        }
    }
}