:- [max].
:- [deleteoccurrence].

deletemax(L,R):-
    max(L,Max),
    deleteoccurrence(L,Max,R).
