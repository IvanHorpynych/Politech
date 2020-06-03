<?php

include_once "./config.php";

$data = [
    "sender" => getCurrentUser(),
    "receiver" => $_POST["receiver"],
    "subject" => $_POST["subject"],
    "content" => $_POST["content"]
];


$xml = new SimpleXMLElement("<xml/>");

foreach ($data as $key => $val) {
    $xml->addChild($key, $val);
}

$fileName = "./emails/". getCurrentUser() ."-". $data["receiver"]. "." . $data["subject"] . "." .date("Y-m-d.H:i:s").".xml";

echo $xml->asXML();

file_put_contents($fileName, $xml->asXML());