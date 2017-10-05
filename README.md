# LightspeedT
This project is a way I development web api sln by used Repository way 


The are two project in sln now,  and will add more about the other 3 or 5  in recently  I expected

LightspeedT it's project builed in Asp.net MVC ,I created a DB by Ms-sql included Two Table 
member and memberdetail .I move the mdf into  app_data file folder.(easy for test and edit)


Model part I used The entity frramework  ORM technique ,gen two object of data-table(POCO)
Beside this added two partial class in partial model file folder ,It's for The meta-data and used Data annotation use.


The other part of model :

Because I'm  use the Repository way to divided the layer. So I have a DAL layer
(context and entitymodel tt)and business logic(service).


I builed  interface<T> to define the usuall method ,define the method detail in GenericRepositiry
  and implement that in my service file folder.


Service:
Used a little be DDD,BDD way to designd
The are two character different behavior ONE is for query ,the other is in charge 
update and delete.



Business logic :

 comming soon !! (like Blizzard lol 
 
 I'll add webservice & WCF in my sln
 







