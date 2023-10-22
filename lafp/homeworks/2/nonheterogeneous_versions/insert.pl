insert([],_,_,_,[],0,0).

insert([H|T],I,E,K,[H|RT0],IR0,ER0):-
    I =:= 2 ** E,
    RT0 = [K|RT1],
    insert(T,I+1,E+1,K,RT1,IR1,ER1),
    IR0 is IR1 + 1,
    ER0 is ER1 + 1.

insert([H|T],I,E,K,[H|RT0],IR0,ER0):-
    I =\= 2 ** E,
    RT0 = RT1,
    insert(T,I+1,E,K,RT1,IR1,ER1),
    IR0 is IR1 + 1,
    ER0 is ER1.

insert(I,K,O,IR,ER):-
    insert(I,1,0,K,O,IR,ER).
