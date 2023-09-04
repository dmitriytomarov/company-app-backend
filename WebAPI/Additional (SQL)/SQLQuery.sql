

-- выборка всех сотрудников

SELECT 
	e.Id
	,e.Name
	,e.Birthday
	,e.WorksFrom
	,e.Salary
	,e.DepartmentId
	,d.Name
FROM Employees e 
	Left Join Departments d on e.DepartmentId = d.Id;


-- сотрудников у кого зп выше 10000

SELECT 
	e.Id
	,e.Name
	,d.Name
	,e.Salary
FROM Employees e 
	Left Join Departments d on e.DepartmentId = d.Id
WHERE e.Salary > 10000;

--удаления сотрудников старше 70 лет 
--!!! надеюсь, имеется ввиду 70 и старше (70 включительно) 

DELETE FROM Employees 
WHERE 
	DATEDIFF(year, Birthday, GetDate()) 
	- (case 
		when MONTH(Birthday)>=MONTH(GetDate()) and Day(Birthday) > Day(GetDate()) 
		then 1 else 0 
		end) >= 70;


--обновить зп до 15000  тем сотрудникам, у которых она меньше.

UPDATE Employees Set Salary = 15000 Where Salary < 15000;