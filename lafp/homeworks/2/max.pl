:-[nonheterogeneous_versions/max].

heterogeneous_max([L1],R):-
    is_list(L1),
    max(L1,R).

heterogeneous_max([L1],R):-
    \+ is_list(L1),
    R is L1.

heterogeneous_max([L1,L2|T],R):-
    is_list(L1),
    is_list(L2),
    max(L1,R1),
    max(L2,R2),
    max(R1,R2,Max),
    heterogeneous_max([Max|T],R).

heterogeneous_max([L1,L2|T],R):-
    is_list(L1),
    \+ is_list(L2),
    max(L1,R1),
    max(R1,L2,Max),
    heterogeneous_max([Max|T],R).

heterogeneous_max([L1,L2|T],R):-
    \+ is_list(L1),
    is_list(L2),
    max(L2,R2),
    max(L1,R2,Max),
    heterogeneous_max([Max|T],R).

heterogeneous_max([L1,L2|T],R):-
    \+ is_list(L1),
    \+ is_list(L2),
    max(L1,L2,Max),
    heterogeneous_max([Max|T],R).
