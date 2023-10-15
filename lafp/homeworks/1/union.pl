:- [deleterepeted].

concat([],[],[]).

concat([HA|TA],B,[HA|RT]):-
    concat(TA,B,RT).

concat([],[HB|TB],[HB|RT]):-
    concat([],TB,RT).

union(A,B,R):-
    concat(A,B,Concat),
    deleterepetedkeepone(Concat,R).
