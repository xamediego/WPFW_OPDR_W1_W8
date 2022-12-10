Feature: Attractie

    Scenario: BestaatAl
        Given attractie Draaimolen bestaat
        When attractie Draaimolen wordt toegevoegd
        Then moet er een error 403 komen

    Scenario: DoesNotExists
        Given attractie Draaimolen not exist
        When attractie Draaimolen gets deleted
        Then moet er een error 404 komen

    Scenario: CreateNew
        Given attractie Draaimolen not exist
        When attractie Draaimolen  created
        Then moet er een error 201 komen