<?php
require 'require/config/config.php';


//wwwForm from Unity
$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$email = $_POST["email"];
$emailClean = filter_var($email, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];
$passhash = password_hash($password, PASSWORD_DEFAULT);

/// Username Check Query
$usernameCheckQuery = "SELECT username FROM players WHERE username = '$usernameClean'";
$userCheckResult = mysqli_query($con, $usernameCheckQuery) or die("2");

if (mysqli_num_rows($userCheckResult) > 0)
{
	echo("3");
	exit();
}

/// Email Check Query
$emailCheckQuery = "SELECT email FROM players WHERE email = '$emailClean'";
$emailCheckResult = mysqli_query($con, $emailCheckQuery) or die("4");

if (mysqli_num_rows($emailCheckResult) > 0)
{
	echo("5");
	exit();
}

$insertUserQuery = "INSERT INTO players(username, email, password) VALUES('$usernameClean', '$emailClean', '$passhash')";

mysqli_query($con, $insertUserQuery) or die("6");
echo("0");

$con->close();


/*Error Codes
1 - Username Check Query ran into an error
2 - User already exists
3 - Email Check Query ran into an error
4 - Email already exists
5 - Insert user failed */
?>