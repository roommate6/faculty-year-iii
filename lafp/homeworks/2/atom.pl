:- [deleteoccurrence].
:- [occurrence].

heterogeneous_atom([],[]).

heterogeneous_atom([H|T],[[H,Occ+1]|RT]):-
    heterogeneous_occurrence(T,H,Occ),
    heterogeneous_edeleteoccurrence(T,H,NewT),
    heterogeneous_atom(NewT,RT).
