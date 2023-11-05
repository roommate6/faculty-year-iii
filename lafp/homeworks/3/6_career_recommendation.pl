skill_career('c++', programmer).
skill_career('java', programmer).
skill_career('python', programmer).
skill_career('rust', programmer).
skill_career('javascript', programmer).
skill_career('typescript', programmer).
skill_career('cs', programmer).
skill_career('web_development', programmer).

skill_career('oop', software_developer).
skill_career('design_pattern', software_developer).

skill_career('react', frontend_developer).
skill_career('angular', frontend_developer).
skill_career('wpf', frontend_developer).
skill_career('css', frontend_developer).
skill_career('html', frontend_developer).

skill_career('cs', backend_developer).
skill_career('c++', backend_developer).
skill_career('java', backend_developer).
skill_career('typescript', backend_developer).
skill_career('javascript', backend_developer).
skill_career('.net', backend_developer).
skill_career('sql', backend_developer).

recommendation(Person, Career) :-
    has_skill(Person, Skill), !,
    skill_career(Skill, Career),
    write('The expert system suggests the next career: '),
    write(Career),
    nl.

has_skill(john, 'c++').
has_skill(john, 'sql').
has_skill(susan, 'angular').
has_skill(susan, 'sql').
