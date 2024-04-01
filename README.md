# refactoring-example
W tym zadaniu Waszym zadaniem jest modyfikacja kodu aplikacji nazwanej LegacyApp. Zakładamy, że jest to pewna spadkowa aplikacja i chcemy poprawić jakość jej kodu. Chcemy, aby kod w projekcie „LegacyAppConsumer” nie uległ modyfikacji. Innymi słowy kodu w tym projekcie nie możemy modyfikować.
Wszystko w projekcie LegacyApp może być modyfikowane – tak długo dopóki nie powoduje to zmiany interfejsu klas wykorzystywanych w projekcie LegacyAppConsumer. Dodatkowo nie można zmodyfikować klasy UserDataAccess i metody statycznej AddUser. Zakładamy, że w pewnych przyczyn nie możemy modyfikować tej klasy.

Proszę wybaczyć bardzo subtelną zmianę w Program.cs, chciałem ładnie poukładać w pakiety. Z wyrazami szacunku, Krzysztof P.