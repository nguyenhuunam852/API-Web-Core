create database ASP_core_api;

use ASP_core_api;

create table users(
   user_id int IDENTITY(1,1) PRIMARY KEY,
   user_name varchar(30) Not null,
   user_password varchar(50) Not null
);


create table roles(
   role_id int IDENTITY(1,1) PRIMARY KEY,
   role_key varchar(10) not null,
   role_description text
);

create table pivot_user_role(
   pivot_id int IDENTITY(1,1) PRIMARY KEY,
   role_id int not null,
   user_id int not null,
   FOREIGN KEY (user_id) REFERENCES users(user_id),
   FOREIGN KEY (role_id) REFERENCES roles(role_id)
);

insert into users values('test','test'); 