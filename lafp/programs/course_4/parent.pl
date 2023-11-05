women(ana).
women(maria).
man(ion).

mother(ana,maria).
mother(ana,ion).

parent(X,Y) :-
    mother(X,Y).
