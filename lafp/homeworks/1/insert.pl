insert([],_,_,_,[]).

insert([H|T],I,E,K,[H|RT0]):-
    I =:= 2 ** E,
    RT0 = [K|RT1],
    insert(T,I+1,E+1,K,RT1).

insert([H|T],I,E,K,[H|RT0]):-
    I =\= 2 ** E,
    RT0 = RT1,
    insert(T,I+1,E,K,RT1).

insert(I,K):-
    insert(I,1,0,K,O),
    write(O).
