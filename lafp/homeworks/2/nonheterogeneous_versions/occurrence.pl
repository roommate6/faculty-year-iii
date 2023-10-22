occurrence([],_,0).

occurrence([H|T],H,O):-
    occurrence(T,H,O2),
    O is O2 + 1.

occurrence([H|T],E,O):-
    H =\= E,
    occurrence(T,E,O).
