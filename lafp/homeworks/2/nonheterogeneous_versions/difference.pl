:- [occurrence].

diff(A,[],A).
diff([],_,[]).

diff([A1|AT],B,[A1|RT]):-
    occurrence(B,A1,Occ),
    Occ =:= 0,
    diff(AT,B,RT).

diff([A1|AT],B,R):-
    occurrence(B,A1,Occ),
    Occ > 0,
    diff(AT,B,R).
