# RssFeeds
##	 Dependencies to build the application:

		dontnet sdk 6.0
		
__Migration:__

* run the following command in nuget package manager or powershell to activate database migration.
```		
	update-database
```				
* or run the following command in CMD  to activate database migration.
```			
	dotnet ef database update
```	

I also included the release build intentionally in case you just want to try the application.
	
## Dependencies to run the application:
*	postgreSQl 13
*	dotnet runtime 6.0.6
*	dotnet hosting 6.0.5
* the script.sql included in the repository is for migrating tables into the database.
	

## Notes		
The repository includes postman collection  "RSSFeeds.postman_collection.json" which provides
some sample requests for testing and understanding the api mechanism.
Port and Url For connecting to API :
```
https://localhost:7002
```

