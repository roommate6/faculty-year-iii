% --- ( + , * )

add(X, Y, Result) :-
    Result is X + Y.

multiply(X, Y, Result) :-
    Result is X * Y.

% --- ( - )

opposite(X, Result) :-
    Result is (-X).

substract(X, Y, Result) :-
    opposite(Y, OppositeOfY),
    add(X, OppositeOfY, Result).

% --- ( / )

inverse(X, Result) :-
    X =\= 0,
    Result is (1 / X).

divide(X, Y, Result) :-
    inverse(Y, InverseOfY),
    multiply(X, InverseOfY, Result).
