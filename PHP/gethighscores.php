<?php
    require("mysql_info.php");
 
    $query = "SELECT * FROM BlackSunEmpire_Schmup ORDER by score DESC LIMIT 5";
    $result = mysql_query($query) or die("Query failed: " . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name'] . "," . $row['score'];

         if ($i != $num_results - 1)
         {
            echo ",";
         }
    }
?>