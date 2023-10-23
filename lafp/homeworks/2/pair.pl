:- [nonheterogeneous_versions/pair].
:- [nonheterogeneous_versions/union].

heterogeneous_elementpairs(_,[],[]).

heterogeneous_elementpairs(E,[H|T],[[E,H]|RT]):-
    \+ is_list(H),
    heterogeneous_elementpairs(E,T,RT).

heterogeneous_elementpairs(E,[H|T],R):-
    is_list(H),
    heterogeneous_elementpairs(E,H,RH),
    heterogeneous_elementpairs(E,T,RT),
    concat(RH,RT,R).

heterogeneous_pairs([],[]).

heterogeneous_pairs([H|T],[RH|RT]):-
    \+ is_list(H),
    heterogeneous_elementpairs(H,T,RH),
    heterogeneous_pairs(T,RT).

heterogeneous_pairs([H|T],R):-
    is_list(H),
    heterogeneous_pairs(H,RH),
    heterogeneous_pairs(T,RT),
    concat(RH,RT,R).
