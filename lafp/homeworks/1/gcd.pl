gcd(X,Y,R):-
    X < 0,
    gcd(-X,Y,R).

gcd(X,Y,R):-
    Y < 0,
    gcd(X,-Y,R).

gcd(X,0,X):-
    X > 0.

gcd(0,Y,Y):-
    Y > 0.

gcd(X,Y,R):-
    X > 0,
    X =:= Y,
    R is X.

gcd(X,Y,R):-
    X > 0,
    X < Y,
    gcd(Y,X,R).

gcd(X,Y,R):-
    X > Y,
    Y > 0,
    X1 is X - Y,
    gcd(X1,Y,R).

gcd([L1],R):-
    L1 < 0,
    gcd([-L1],R).

gcd([L1],R):-
    L1 > 0,
    R is L1.

gcd([L1,L2|T],R):-
    gcd(L1,L2,H),
    gcd([H|T],R).
