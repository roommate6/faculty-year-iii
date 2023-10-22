:- [nonheterogeneous_versions/union].
:- [nonheterogeneous_versions/deleteoccurrence].

heterogeneous_deleteoccurrence([],_,[]).

heterogeneous_deleteoccurrence([H|T],X,R):-
    is_list(H),
    deleteoccurrence(H,X,RH),
    heterogeneous_deleteoccurrence(T,X,RT),
    concat(RH,RT,R).

heterogeneous_deleteoccurrence([H|T],X,[H|RT]):-
    \+ is_list(H),
    X =\= H,
    heterogeneous_deleteoccurrence(T,X,RT).

heterogeneous_deleteoccurrence([H|T],X,RT):-
    \+ is_list(H),
    X =:= H,
    heterogeneous_deleteoccurrence(T,X,RT).
