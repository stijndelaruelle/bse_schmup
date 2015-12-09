<?php 
    require("mysql_info.php");

    // Strings must be escaped to prevent SQL injection attack. 
    $name = mysql_real_escape_string($_GET['name']);
    $score = mysql_real_escape_string($_GET['score']);
    $email = mysql_real_escape_string($_GET['email']);
    $hash = $_GET['hash']; 

    //check for a valid e-mail adress
    //if(ereg("^[-A-Za-z0-9_]+[-A-Za-z0-9_.]*[@]{1}[-A-Za-z0-9_]+[-A-Za-z0-9_.]*[.]{1}[A-Za-z]{2,5}$", $email)){}
    //else{$errormsg .= "Please enter a valid <b>e-mail</b>.<br>";}

    $secretKey = "sbasletsscamlhtuspasletcsraelttkey"; //bseschmupsecretkey with salt in between

    $real_hash = md5($name . $score . $email . $secretKey);
    $ip = $_SERVER['REMOTE_ADDR'];

    if($real_hash == $hash)
    {
    mysql_query("INSERT INTO BlackSunEmpire_Schmup (id, name, score, email, date, ip)
                 VALUES('', '$name', '$score', '$email', now(), '$ip')");
    }
    else
    {
        echo("Hashes didn't match!");
    } 
?>