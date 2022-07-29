select * from users;
create database VideoAspCore;
use VideoAspCore;

insert into users(user_name,user_password) values ('evannguyen12399@gmail.com','test')


insert into roles(role_key,role_description,parent_id) values ('admin','admin',null);
insert into roles(role_key,role_description,parent_id) values ('staff','staff',(select top 1 role_id from roles where role_key = 'admin'));
insert into roles(role_key,role_description,parent_id) values ('instructor','instructor',(select top 1 role_id from roles where role_key = 'admin'));
insert into roles(role_key,role_description,parent_id) values ('manager','manager',(select top 1 role_id from roles where role_key = 'admin'));
insert into roles(role_key,role_description,parent_id) values ('student','student',(select top 1 role_id from roles where role_key = 'instructor'));

  insert into PivotUserRole (UserId,RoleId) values (1,1);
  RoleId	PermissionId

DECLARE @Names VARCHAR(8000)

WITH previous(Id, [Key], [path]) AS (
  SELECT p.permission_id as Id,
         p.permission_key as [Key],
		 '/' + convert(varchar(max), case when roles.parent_id is not null then roles.parent_id else 0 end) + '/' [path]
  FROM   roles roles 
  join PivotUserRole pur on roles.role_id = pur.RoleId
  join PivotRolePermission prp on roles.role_id = prp.RoleId
  join [permissions] p on permission_id = prp.PermissionId
  WHERE  pur.UserId = 1 
  UNION ALL
  SELECT curTable.permission_id as Id,
		 curTable.permission_key as [Key],
		 pr.[path] + convert(varchar(max),curTable.parent_id) + '/'
  FROM   (
     select curRoles.parent_id as parent_id,p.permission_id,p.permission_key from
     roles curRoles 
	 join PivotRolePermission prp on curRoles.role_id = prp.RoleId
     join [permissions] p on permission_id = prp.PermissionId
  ) curTable, previous pr
  WHERE  curTable.parent_id = pr.Id 
  and pr.[path] not like '%/' + rtrim(curTable.parent_id) + '/%' 
)
SELECT distinct previous.[Key]
FROM   previous;

