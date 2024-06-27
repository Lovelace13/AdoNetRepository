1.	Crear proyecto asp.net core mvc
2.	Install-Package System.Data.SqlClient
3.	Configurar la cadena de conexion 
select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
from sys.server_principals
where name = suser_name()
4.	Crear la interfaces en un proyecto aparte llamado Infrastructure
5.	Crear el repositorio (Base repositorio) para hacer las llamadas a base
6.	Crear el sp que haga los llamados select * from
