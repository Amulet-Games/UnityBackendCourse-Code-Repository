<?php
require 'require/config/config.php';


$search = $_POST["search"];
$searchClean = filter_var($search, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_HIGH);

$searchQuery = "SELECT username, score FROM players WHERE username LIKE '%$searchClean%' ORDER BY score DESC";
$searchQueryResult = $con->query($searchQuery) or die("2");
if ($searchQueryResult->num_rows > 0)
{
	$json_array = array();
	while($row = mysqli_fetch_assoc($searchQueryResult))
	{
		$json_array[] = $row;
	}

	echo json_encode($json_array);
}
else
{
	echo("3");
}

$con->close();


/*Error Codes
1 - Error With The Search
2 - No Results Found */
?>