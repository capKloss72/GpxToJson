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

insert into RaceDetails values
	('hr_16_1','1. Gene Çve - Crans Montana',0),	
	('hr_16_2','2. Crans Montana - San Gottardo',0),	
	('hr_16_3','3. St Moritz - Bormio Passo Stelvio',0),	
	('hr_16_4','4. Passo dello Stelvio',0),	
	('hr_16_5','5. Bormio - Bolzano',0),	
	('hr_16_6','6. Bolzano - Cortina d''Ampezzo Tre Cime di Lavaredo',0),	
	('hr_16_7','7. Cortina d''Ampezzo - Conegliano - Venise',0)

select * from dbo.RiderDetails