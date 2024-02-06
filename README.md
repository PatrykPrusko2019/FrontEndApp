# FrontEndApp
Aplikacja Frontendowa dla WebApi, Opis aplikacji w katalogu FrontEndApp/FrontEndApp
/DescriptionOfApp -> Aplikacja Frontendowa dla Web Api.pdf , cały opis działania aplikacji, która łączy się z danym Web Api z githuba : https://github.com/PatrykPrusko2019/WebApiForCompanyZintegrujemy.pl.git .
Jeśli chodzi jeszcze o wyszukiwanie za pomocą szukanego słowa względem kolumny Id, to najlepiej , wybrać przycisk '<<' -> firstPage -> potem wyszukać po np. numerze 13, to dobrze działa. Potem można przeglądać na różnych stronach wyniki.


Aplikacja udostępniona na chmurze Azure, to jest wersja programu która jest raze z aplikacją Frontendowa.
Link do niej : https://product-api-app.azurewebsites.net/swagger/index.html
Można się z nią też połączyć z aplikacji Frontend tylko trzeba zmienić w katalogu FrontEndApp/Utilites/HelperHttpClient trzeba zmienić adres uri na : private const string uri = @"https://product-api-app.azurewebsites.net/"; // azure connection
 => wtedy tylko uruchomić aplikacje FrontEnd i można działać.  Już jest nawet dostępne konto email: patrykprusko@gmail.com, password: password1 , można przetestować. Link do Aplikacj Frontend : https://github.com/PatrykPrusko2019/FrontEndApp.git
