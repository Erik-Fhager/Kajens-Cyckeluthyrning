Analysera och åtgärda problem
En bugg jag hittade var att nya användare inte kunde hyra cyklar om man inte startade om programmet efter att man hade gjort en ny användare.  
Orsaken var att programmet läste Json-filen innan loopen började och inte efter att man hade registrerat en ny användare. 
Sättet jag löste det var att jag satte in en läsfunktion i kund registreringsfunktionen.

En förbättring man kan göra är att man inte kan ha samma kundnamn på olika kunder eller att man separerar dem, 
så att man kan välja vilken kund man vill ha istället för att programmet väljer den tidigaste av kunderna med samma namn.
Jag implementerade detta i min kod där man inte får ha samma namn på olika kunder. 
Eftersom detta projektet är till butiken så får de ett felmeddelande om samma namn används. Man kan också lösa detta genom att ha med en kund id på varje kund.
