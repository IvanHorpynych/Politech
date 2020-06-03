<?php

include_once "./config.php";

$fileName = $_GET['file'];

$files = scandir('./emails');

$exists = false;

foreach ($files as $file) {
    if ($file === $fileName) {
        $exists = true;
    }
}

echo "<a href='/'>Home</a><br>";
if (!$exists) {
    echo "File not found. <br>";
    exit(0);
}

$email = new SimpleXMLElement(file_get_contents('./emails/' . $fileName));

?>


<table>
    <tbody>
        <tr>
            <td>From:</td>
            <td><?= $email->sender ?></td>
        </tr>
        <tr>
            <td>To:</td>
            <td><?= $email->receiver ?></td>
        </tr>
        <tr>
            <td>Subject:</td>
            <td><?= $email->subject ?></td>
        </tr>
        <tr>
            <td>Content:</td>
            <td><?= $email->content ?></td>
        </tr>
    </tbody>
</table>