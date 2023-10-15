max(L1,L2,R):-
    L1 > L2,
    R is L1.

max(L1,L2,R):-
    L1 =< L2,
    R is L2.

max([L1],R):-
    R is L1.

max([L1,L2|T],R):-
    max(L1,L2,Max),
    max([Max|T],R).
