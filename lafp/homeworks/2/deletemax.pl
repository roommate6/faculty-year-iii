:- [max].
:- [deleteoccurrence].

heterogeneous_deletemax(L,R):-
    heterogeneous_max(L,Max),
    heterogeneous_deleteoccurrence(L,Max,R).
