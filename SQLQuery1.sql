create table dbo.RaceDetails
(
	RaceId varchar(50) not null primary key,
	RaceName varchar(255) not null,
	IsCurrent int not null
)

create table dbo.RiderDetails
(
	RiderId varchar(50) not null primary key,
	Name varchar(255) not null,
	Team varchar(255) not null,
	IsCurrent int not null
)

insert into RiderDetails values
	(18,'Rider 18','Satalyst',0),
	(181,'Graeme Brown','Drapac',0),
	(182,'Samuel Spokes','Drapac',0),
	(183,'Nathan Earle','Drapac',0),
	(184,'Brenton Jones','Drapac',0),
	(185,'Gavin Mannion','Drapac',0),
	(186,'Lachlan Norris','Drapac',0),
	(187,'Adam Phelan','Drapac',0)

select * from RiderDetails
