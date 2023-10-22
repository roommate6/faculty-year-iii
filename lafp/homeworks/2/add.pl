:- [nonheterogeneous_versions/add].
:- [nonheterogeneous_versions/union].

heterogeneous_add([],[]).

heterogeneous_add([H|T],R):-
    is_list(H),
    add(H,RH),
    heterogeneous_add(T,RT),
    concat(RH,RT,R).

heterogeneous_add([H|T],[H,1|RT]):-
    \+ is_list(H),
    heterogeneous_add(T,RT).
