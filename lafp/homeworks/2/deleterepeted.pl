:- [deleteoccurrence].
:- [occurrence].

heterogeneous_deleterepeted([],[]).

heterogeneous_deleterepeted([H|T],R):-
    heterogeneous_occurrence(T,H,Occ),
    Occ > 0,
    heterogeneous_deleteoccurrence(T,H,NewT),
    heterogeneous_deleterepeted(NewT,R).

heterogeneous_deleterepeted([H|T],[H|RT]):-
    heterogeneous_occurrence(T,H,Occ),
    Occ =:= 0,
    heterogeneous_deleterepeted(T,RT).
