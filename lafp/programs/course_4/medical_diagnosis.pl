symptom_disease(fever, flu).
symptom_disease(cough, flu).
symptom_disease(runny_nose, flu).

symptom_disease(fever, cold).
symptom_disease(sore_throat, cold).
symptom_disease(runny_nose, cold).

recommendation(flu) :-
    write('Recommendation: Rest, take medicine for fever and cough, drink plenty of fluids.').

recommendation(cold) :-
    write('Recommendation: Rest, take medicine for fever and sore throat, drink plenty of fluids.').

diagnosis(Patient, Disease) :-
    has_symptom(Patient, Symptom), !,
    symptom_disease(Symptom, Disease),
    write('The expert system diagnosed the patient with: '),
    write(Disease),
    nl,
    recommendation(Disease),
    nl.

has_symptom(john, fever).
has_symptom(john, cough).
has_symptom(susan, fever).
has_symptom(susan, sore_throat).
