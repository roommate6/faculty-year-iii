elementpairs(_,[],[]).

elementpairs(E,[H|T],[[E,H]|RT]):-
    elementpairs(E,T,RT).

pairs([],[]).

pairs([H|T],[RH|RT]):-
    elementpairs(H,T,RH),
    pairs(T,RT).
