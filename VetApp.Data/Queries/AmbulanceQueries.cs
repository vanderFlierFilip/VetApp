using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Data.Queries
{
    public static class AmbulanceQueries
    {
        public static string GetOwnersWithPetsQuery(string orderBy) =>
            @$"select o.*, p.id as pet_id, p.name as pet_name from owners as o
                join (select pets.id, pets.owner_id, pets.name from pets) as p on o.id = p.owner_id
                where strpos( lower(o.name), @searchTerm) > 0
                order by {orderBy}
                offset @skip rows
                fetch first @take row only;";

        public const string GetOwnerWithPetsQuery = @"select o.*, p.id as pet_id, p.name as pet_name from owners as o
                join (select pets.id, pets.owner_id, pets.name from pets) as p on o.id = p.owner_id
                where o.id = @ownerId";

        public const string GetDiagnosesQuery =
            @"select * from diagnoses;";

        public const string GetPetByOwnerAndPetIdQuery = @"SELECT pets.id, pets.chip_number, pets.name, pets.date_of_birth, pets.sex, pets.last_visit as last_checkup, pets.description, pets.alergy, breeds.breed, species.species FROM pets
                JOIN (SELECT breeds.id, breeds.name as breed FROM breeds) as breeds ON breeds.id = pets.breed_id
                JOIN (SELECT species.id, species.name as species FROM species) as species ON species.id = pets.species_id
                WHERE pets.id = @ownerId and owner_id = @petId;";

        public const string GetOwnerByIdQuery = @"SELECT * FROM owners where id = @ownerId;";

        public const string GetAllPetsExceptOneByIdQuery = @"SELECT id, name FROM pets WHERE pets.owner_id = @ownerId AND pets.id != @petId;";

        public const string GetAllCheckupsForPetBasedOnType =
                @"SELECT
	                checkups.*, cd.medicament_id, cd.checkup_id as split, cd.amount, cd.medicament_id, cd.application, cd.uom, medicaments.name
                FROM checkups
                    JOIN (SELECT checkup_id, medicament_id, amount, uom, application from checkup_details) as cd
                    ON cd.checkup_id = checkups.id
                    JOIN medicaments ON medicaments.id = cd.medicament_id
                    WHERE petid = @petId
                    AND (@checkupType IS NULL OR checkups.checkup_type = @checkupType)
                ORDER BY date DESC";
    }
}