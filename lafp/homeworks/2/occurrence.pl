:-[nonheterogeneous_versions/occurrence].

heterogeneous_occurrence([],_,0).

heterogeneous_occurrence([H|T],X,R):-
    is_list(H),
    occurrence(H,X,RH),
    heterogeneous_occurrence(T,X,RT),
    R is RH + RT.

heterogeneous_occurrence([H|T],X,R):-
    number(H),
    H =\= X,
    heterogeneous_occurrence(T,X,R).

heterogeneous_occurrence([H|T],X,R):-
    number(H),
    H =:= X,
    heterogeneous_occurrence(T,X,RT),
    R is RT + 1.

heterogeneous_occurrence([H|T],X,R):-
    atom(H),
    heterogeneous_occurrence(T,X,R).
