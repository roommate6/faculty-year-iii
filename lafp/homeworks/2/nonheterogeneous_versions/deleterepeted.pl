:- [deleteoccurrence].
:- [occurrence].

deleterepeted([],[]).

deleterepeted([H|T],R):-
    occurrence(T,H,Occ),
    Occ > 0,
    deleteoccurrence(T,H,NewT),
    deleterepeted(NewT,R).

deleterepeted([H|T],[H|RT]):-
    occurrence(T,H,Occ),
    Occ =:= 0,
    deleterepeted(T,RT).

% ---

deleterepetedkeepone([],[]).

deleterepetedkeepone([H|T],R):-
    occurrence(T,H,Occ),
    Occ >= 1,
    deleteoccurrence(T,H,NewT),
    deleterepetedkeepone([H|NewT],R).

deleterepetedkeepone([H|T],[H|RT]):-
    occurrence(T,H,Occ),
    Occ =:= 0,
    deleterepetedkeepone(T,RT).
