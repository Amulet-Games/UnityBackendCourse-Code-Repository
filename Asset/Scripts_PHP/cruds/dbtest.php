<?php

$servername = "localhost";
$username = "app";
$password = "XhO_InXSh]ht9PQu";
$dbname = "unitybackend";

$con = mysqli_connect($servername, $username, $password, $dbname);
if (mysqli_connect_error())
{
	echo("1");
	exit();
}
else
{
	echo("2");
}

?>