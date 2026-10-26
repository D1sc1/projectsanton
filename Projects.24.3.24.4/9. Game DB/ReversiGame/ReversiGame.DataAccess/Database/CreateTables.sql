-- Таблицы для варианта доступа к данным через ADO.NET (строка подключения "Main").
-- Выполнить в pgAdmin в пустой базе данных.

create table if not exists "Users"
(
    "Id"     serial primary key,
    "Name"   text not null unique,
    "Wins"   integer not null default 0,
    "Losses" integer not null default 0,
    "Draws"  integer not null default 0
);

create table if not exists "Games"
(
    "Id"             serial primary key,
    "UserId"         integer not null references "Users" ("Id") on delete cascade,
    "MoveCount"      integer not null default 0,
    "CurrentPlayer"  integer not null default 1,
    "LastMoveRow"    integer not null default -1,
    "LastMoveColumn" integer not null default -1
);

-- Хранятся только занятые клетки доски: Value = 1 (черная фишка) или 2 (белая)
create table if not exists "Cells"
(
    "Id"     serial primary key,
    "GameId" integer not null references "Games" ("Id") on delete cascade,
    "Row"    integer not null,
    "Column" integer not null,
    "Value"  integer not null,
    unique ("GameId", "Row", "Column")
);

create index if not exists "IX_Games_UserId" on "Games" ("UserId");
create index if not exists "IX_Cells_GameId" on "Cells" ("GameId");
