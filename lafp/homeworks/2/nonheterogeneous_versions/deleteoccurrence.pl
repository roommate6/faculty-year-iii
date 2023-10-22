deleteoccurrence([],_,[]).

deleteoccurrence([H|T],X,[H|RT]):-
    X =\= H,
    !,
    deleteoccurrence(T,X,RT).

deleteoccurrence([H|T],H,RT):-
    !,
    deleteoccurrence(T,H,RT).
