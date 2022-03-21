<?php
require 'require/config/config.php';


//wwwForm from Unity
$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];

$usernameCheckQuery = "SELECT * FROM players WHERE username = '$usernameClean'";
$usernameCheckResult = mysqli_query($con, $usernameCheckQuery) or die("2");

if ($usernameCheckResult->num_rows != 1)
{
	echo("3");
	exit();
}
else
{
	$fetchedPassword = mysqli_fetch_assoc($usernameCheckResult)["password"];
	if (password_verify(($password), $fetchedPassword))
	{
		$playerInfo = "SELECT * FROM players WHERE username = '$usernameClean'";
		$playerInfoResult = mysqli_query($con, $playerInfo) or die("5");
		$existingPlayerInfo = mysqli_fetch_assoc($playerInfoResult);
		$playerUsername = $existingPlayerInfo["username"];
		$playerScore = $existingPlayerInfo["score"];
		echo($playerUsername. ":". $playerScore);
	}
	else
	{
		echo("4");
	}
}

$con->close();


/*Error Codes
1 - Username query error
2 - Username not existing or there's more than 1 in the table
3 - Password was not able to be verified
4 - Player Info query Failed */
?>