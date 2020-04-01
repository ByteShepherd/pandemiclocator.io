create table if not exists healthreport
(
	id uuid default (md5(((random())::text || (clock_timestamp())::text)))::uuid not null
		constraint healthreport_pk
			primary key,
	identifier varchar not null,
	quantity integer not null,
	status integer not null,
	source integer not null,
	latitude double precision not null,
	longitude double precision not null,
	"when" timestamp with time zone not null
);

alter table healthreport owner to pandemic;

create unique index if not exists healthreport_id_uindex
	on healthreport (id);

