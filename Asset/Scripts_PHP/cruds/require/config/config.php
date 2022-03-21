<?php

$servername = "localhost";
$username = "root";
$password = "hoge";
$dbname = "unitybackend";

$con = mysqli_connect($servername, $username, $password, $dbname, 3307);
if (mysqli_connect_errno())
{
	echo("0");
	exit();
}

//App key
$appkey = $_POST["apppassword"];
if ($appkey != "thisisfromtheapp")
{
	exit();
}


/*Error Codes
0 - Database Connection Error */

?>