## Per-request:
1. dotnet 9.0 sdk & runtime

## Before start the program:
Please follow the ./OnyxProductAPI/envSettings.example to create a envSettings.json file under OnyxProductAPI directory (i.e. same level with program.cs), please either use detail from azure active direct or use rand text to replace {your azure ad}.

## Use API
Due to the authentication, we need either add bearer token form Azure,
or we need to add X-API-KEY to header with your secret

### simple diagram
in root folder ./product service architect.png