:- [deleteoccurrence].
:- [occurrence].

atom([],[]).

atom([H|T],[[H,Occ+1]|RT]):-
    occurrence(T,H,Occ),
    deleteoccurrence(T,H,NewT),
    atom(NewT,RT).
