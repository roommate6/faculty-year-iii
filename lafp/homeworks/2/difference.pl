:- [occurrence].
:- [nonheterogeneous_versions/difference].
:- [nonheterogeneous_versions/union].

heterogeneous_diff(A,[],A).
heterogeneous_diff([],_,[]).

heterogeneous_diff([A1|AT],B,[A1|RT]):-
    \+ is_list(A1),
    heterogeneous_occurrence(B,A1,Occ),
    Occ =:= 0,
    heterogeneous_diff(AT,B,RT).

heterogeneous_diff([A1|AT],B,R):-
    \+ is_list(A1),
    heterogeneous_occurrence(B,A1,Occ),
    Occ > 0,
    heterogeneous_diff(AT,B,R).

heterogeneous_diff([A1|AT],B,R):-
    is_list(A1),
    heterogeneous_diff(A1,B,RA1),
    heterogeneous_diff(AT,B,RT),
    concat(RA1,RT,R).
