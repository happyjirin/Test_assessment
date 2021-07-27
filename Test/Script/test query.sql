select p.UniqueName as PlaformName,w.* from platform p 
join well w on w.PlatformId = p.Id
where w.UpdatedAt in (select MAX(updatedat) from well group by platformid)
order by p.uniquename,w.UpdatedAt DESC