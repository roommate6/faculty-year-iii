:- [nonheterogeneous_versions/insert].
:- [nonheterogeneous_versions/union].

heterogeneous_insert([],_,_,_,[]).

heterogeneous_insert([H|T],I,E,K,R):-
    is_list(H),
    insert(H,I,E,K,RH,RI,RE),
    heterogeneous_insert(T,I+RI,E+RE,K,RT),
    concat(RH,RT,R).

heterogeneous_insert([H|T],I,E,K,[H|RT]):-
    \+ is_list(H),
    I =\= 2 ** E,
    heterogeneous_insert(T,I+1,E,K,RT).

heterogeneous_insert([H|T],I,E,K,[H|RT0]):-
    \+ is_list(H),
    I =:= 2 ** E,
    RT0 = [K|RT1],
    heterogeneous_insert(T,I+1,E+1,K,RT1).

heterogeneous_insert([],_,[]).

heterogeneous_insert(I,K,O):-
    heterogeneous_insert(I,1,0,K,O).
