namespace VDMJasminka.Data.Queries
{
    public static class MedicamentInventoryQueries
    {
        public const string GetMedicamentsInStock =
            @"select distinct medicament_id, medicament_name, current_amount, uom, price
            from medicament_inventory where current_amount > 0 order by medicament_name;";

        public const string GetInventoryWithdrawals =
            @"select checkups.id as id, checkups.checkup_type as withdrawal_type, checkup_details.medicament_id,
                checkup_details.amount, checkups.date from checkup_details
                    join checkups on checkup_details.checkup_id = checkups.id
                  where medicament_id =@medicament_id;";

        public const string GetInventoryOrderEntries =
            @"select supply_receptions_mv.id as supply_reception_id, supply_receptions_mv.description as order_no, supply_receptions_mv.amount, supply_receptions_mv.date
                from medicament_inventory
                 join (
                  select id, notes, amount, supply_reception_id, date, medicament_id, sr_mv.description
                  from supply_reception_details
                  join (select id, date, description from supply_receptions) AS sr_mv
                  on sr_mv.id = supply_reception_details.supply_reception_id
                ) as supply_receptions_mv
                on supply_receptions_mv.medicament_id = medicament_inventory.medicament_id
                where medicament_inventory.medicament_id = @medicament_id;";

        public const string GetInventoryAdjustments =
            @"select adjustments_mv.amount, adjustments_mv.reason , adjustments_mv.date, adjustments_mv.id
                from medicament_inventory
                inner join (
	                select id, medicament_id, amount, reason, date
	                from inventory_adjustments
                ) as adjustments_mv on adjustments_mv.medicament_id = medicament_inventory.medicament_id
                where medicament_inventory.medicament_id =@medicament_id;
                ";

        public const string CreateInventoryView =
            @"CREATE VIEW medicament_inventory AS SELECT medicaments.id AS medicament_id,
            medicaments.name AS medicament_name,
            t_e.supplier_id,
            tb_s.supplier_name,
            medicaments.material,
            medicaments.minimal_amount,
            COALESCE(t_e.total_entries::double precision - t_w.total_withdrawals + COALESCE(t_a.total_adjustments, 0::double precision), 0::double precision) AS current_amount,
            medicaments.uom,
            medicaments.price
           FROM medicaments
             LEFT JOIN ( SELECT srd.medicament_id,
                    sr.supplier_id,
                    COALESCE(sum(srd.amount), 0::numeric) AS total_entries
                   FROM supply_reception_details srd
                     LEFT JOIN ( SELECT supply_receptions.id,
                            supply_receptions.supplier_id,
                            supply_receptions.date,
                            supply_receptions.description
                           FROM supply_receptions) sr ON srd.supply_reception_id = sr.id
                  GROUP BY srd.medicament_id, sr.supplier_id) t_e ON medicaments.id = t_e.medicament_id
             LEFT JOIN ( SELECT checkup_details.medicament_id,
                    sum(checkup_details.amount) AS total_withdrawals
                   FROM checkup_details
                  GROUP BY checkup_details.medicament_id) t_w ON medicaments.id = t_w.medicament_id
             LEFT JOIN ( SELECT suppliers.id,
                    suppliers.name AS supplier_name
                   FROM suppliers) tb_s ON tb_s.id = t_e.supplier_id
             LEFT JOIN ( SELECT adj.medicament_id,
                    sum(COALESCE(adj.amount, 0::double precision)) AS total_adjustments
                   FROM inventory_adjustments adj
                  GROUP BY adj.medicament_id) t_a ON t_a.medicament_id = medicaments.id
          ORDER BY medicaments.id;";

        public const string GetCurrentAmountOfMedicamentFromInventoryView =
            @"SELECT current_amount FROM medicament_inventory WHERE medicament_id = @medicament_id";

        public const string DropInventoryView =
            @"DROP VIEW medicament_inventory";

        public const string GetSingleItem =
           @"SELECT medicament_id, medicament_name, supplier_id, supplier_name, material, current_amount,
            minimal_amount, uom, price, sp.phone_number as supplier_phone_number, sp.street_address as supplier_address, sp.location as supplier_location
            FROM medicament_inventory
            LEFT JOIN suppliers as sp on sp.id = medicament_inventory.supplier_id
            WHERE medicament_id = @medicament_id";

        public const string GetBalnceForSupplier =
                    @"SELECT
                        (sum(total) - spending.total_spending) AS balance
                            FROM
                            (

                            SELECT
	                            supply_receptions.*,
	                            sum(srd.total_price) AS total
                            FROM
	                            supply_receptions
	                            JOIN (

		                            SELECT
			                            supply_reception_id,
			                            (price * amount) AS total_price,
			                            price,
			                            amount
		                            FROM
			                            supply_reception_details
	                            ) AS srd ON srd.supply_reception_id = supply_receptions.id
                            GROUP BY
	                            supply_receptions.id
                            ) AS totals
                            JOIN (

                            SELECT
	                            supplier_id,
	                            sum(amount) AS total_spending
                            FROM
	                            supplier_expenses
                            GROUP BY
	                            supplier_id
                            ) AS spending ON spending.supplier_id = totals.supplier_id
		                WHERE totals.supplier_id = @supplierId
                        GROUP BY
                        totals.supplier_id,
                        spending.supplier_id,
                        spending.total_spending;";

        public const string GetMedicamentsSuppliedBySupplier = @"SELECT
	                                                                medicaments.id,
	                                                                medicaments.name,
                                                                    medicaments.price
                                                                FROM
	                                                                medicaments
	                                                                JOIN supply_reception_details srd ON medicaments.id = srd.medicament_id
	                                                                JOIN supply_receptions sr ON sr.id = srd.supply_reception_id
                                                                WHERE
	                                                                sr.supplier_id = @supplierId
                                                                GROUP BY
	                                                                medicaments.id;";

        public static string GetSuppliers(string orderBy)
        {
            return @$"
                    SELECT
	                    *
                    FROM
	                    suppliers
                    WHERE
	                    strpos(lower(suppliers.name), @searchTerm) > 0
                    ORDER BY
	                    {orderBy} OFFSET @skip ROWS
                    FETCH FIRST
	                    @take ROW ONLY;
                   ";
        }

        public const string GetSupplierRecievedOrders = @"SELECT
	                                                        description,
	                                                        date
                                                        FROM
	                                                        supply_receptions
                                                        WHERE
	                                                        supplier_id = @supplierId
                                                        ORDER BY
	                                                        date DESC";
    }
}