<?php
require 'require/config/config.php';


//wwwForm from Unity
$username = $_POST["username"];
$score = $_POST["score"];

$usernameCheckQuery = "SELECT * FROM players WHERE username = '$username'";
$usernameCheckResult = mysqli_query($con, $usernameCheckQuery) or die("2");

if ($usernameCheckResult->num_rows != 1)
{
	echo("3");
	exit();
}

$updateUserQuery = "UPDATE players SET score = $score WHERE username = '$username'";
mysqli_query($con, $updateUserQuery) or die("4");

echo("0");
$con->close();


/*Error Codes
1 - Username query error
2 - Username not existing or there's more than 1 in the table
3 - Update score failed*/
?>