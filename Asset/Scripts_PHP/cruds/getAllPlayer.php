<?php
require 'require/config/config.php';


$tableQuery = "SELECT username, score FROM players ORDER BY score DESC";
$tableQueryResult = $con->query($tableQuery) or die("2");

if ($tableQueryResult->num_rows > 0)
{
	$json_array = array();
	while($row = mysqli_fetch_assoc($tableQueryResult))
	{
		$json_array[] = $row;
	}
	echo json_encode($json_array);
}
else
{
	echo("3");
}


/*Error Codes
1 - Table Query Error
2 - Result didn't have an info */
?>