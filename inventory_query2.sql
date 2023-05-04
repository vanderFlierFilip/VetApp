 SELECT medicaments.id AS medicament_id,
 	medicaments.name AS medicament_name,
    t_e.supplier_id,
	tb_s.supplier_name,
	medicaments.material,
    medicaments.minimal_amount AS min_amount,
    COALESCE(t_e.total_entries::double precision - t_w.total_withdrawals + t_a.total_adjustments , 0::double precision) AS curr_amount,
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
	LEFT JOIN ( SELECT suppliers.id, suppliers.name AS supplier_name FROM suppliers) AS tb_s ON tb_s.id = t_e.supplier_id
	LEFT JOIN ( SELECT adj.medicament_id, SUM(adj.amount) AS total_adjustments FROM inventory_adjustments adj
			   GROUP BY adj.medicament_id) AS t_a ON t_a.medicament_id = medicaments.id
   ORDER BY medicaments.id ASC
			   

	

	
	