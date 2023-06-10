# RestaurantApp
Projekat za predmet Web Programiranje ASP - Dokumentacija

U ovom projektu napravljena je aplikacija koja se koristi u Restoranu za usluzivanje i rezervisanje. Poenta aplikacije je da ubrza rad Konobara i Recepcionara. 
Postoje 3 uloge:
- Administrator 
- Konobar (Waiter)
- Recepcioner (Receptionist)

Database Screenshot:
![image](https://github.com/datchavelli/RestaurantApp/assets/40506719/5d74986c-64f9-4cf2-b891-9fcca3be30c0)

Podeljeni su UseCase-ovi za svakog od korisnika.
Recepcioner moze samo da se bavi (insert, update, delete) rezervacijama i da samo pregledava porudzbine, dok Konobar moze da se bavi samo porudzbinama, a da pregledava rezervacije.
Administrator ima sva prava i moze da menja, dodaje i brise stvari koje ne mogu ni recepcioner ni konobar.

Funkcionalnosti:
- Registracija Korisnika
- Dohvatanje i Izlistavnaje Korisnika, Stolova, Porudzbina, Rezervacija, Proizvoda sa Menija, Kategorija, i Logova
- Brisanje i izmena gore napomenutih stavki. (Odradjen je "soft" delete)
- Menjanje stanja porudzbina, rezervacija i stolova.

Ukupno ima 30 UseCase-ova i administrator ima prava da obavi svaki. 
Pokretanjem Initial Kontrolera se popunjuje baza.
