:-[nonheterogeneous_versions/gcd].

heterogeneous_gcd([L1],R):-
    number(L1),
    gcd([L1],R).

heterogeneous_gcd([L1],R):-
    is_list(L1),
    gcd(L1,R).

heterogeneous_gcd([L1,L2],R):-
    number(L1),
    number(L2),
    gcd([L1,L2],R).

heterogeneous_gcd([L1,L2],R):-
    is_list(L1),
    number(L2),
    gcd(L1,R1),
    gcd([R1,L2],R).

heterogeneous_gcd([L1,L2],R):-
    number(L1),
    is_list(L2),
    gcd(L2,R2),
    gcd([L1,R2],R).

heterogeneous_gcd([L1,L2],R):-
    is_list(L1),
    is_list(L2),
    gcd(L1,R1),
    gcd(L2,R2),
    gcd([R1,R2],R).

heterogeneous_gcd([L1,L2],R):-
    atom(L1),
    \+ atom(L2),
    heterogeneous_gcd(L2,R).

heterogeneous_gcd([L1,L2],R):-
    \+ atom(L1),
    atom(L2),
    heterogeneous_gcd(L1,R).

heterogeneous_gcd([L1,L2|T],R):-
    heterogeneous_gcd([L1,L2],RH),
    heterogeneous_gcd([RH|T],R).
