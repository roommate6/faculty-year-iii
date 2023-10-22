add([],[]).

add([H|T],[H,1|RT]):-
    add(T,RT).
