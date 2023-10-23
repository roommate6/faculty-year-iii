:- [deleterepeted].
:-[nonheterogeneous_versions/union].

heterogeneous_concat([],[],[]).

heterogeneous_concat([HA|TA],B,[HA|RT]):-
    \+ is_list(HA),
    heterogeneous_concat(TA,B,RT).

heterogeneous_concat([],[HB|TB],[HB|RT]):-
    \+ is_list(HB),
    heterogeneous_concat([],TB,RT).

heterogeneous_concat([HA|TA],B,R):-
    is_list(HA),
    heterogeneous_concat(HA,[],RHA),
    heterogeneous_concat(TA,B,RTA),
    concat(RHA,RTA,R).

heterogeneous_concat([],[HB|TB],R):-
    is_list(HB),
    heterogeneous_concat(HB,[],RHA),
    heterogeneous_concat([],TB,RTB),
    concat(RHA,RTB,R).

heterogeneous_union(A,B,R):-
    heterogeneous_concat(A,B,Concat),
    heterogeneous_deleterepetedkeepone(Concat,R).
