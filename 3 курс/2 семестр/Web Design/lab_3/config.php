<?php
error_reporting(E_ALL);
ini_set("display_errors", 1);
session_start();

$CONFIGS = [
  "userData" => [
      "1" => [
          "id" => 1,
          "login" => "user1",
          "password" => md5("password")
      ],
      "2" => [
          "id" => 2,
          "login" => "user2",
          "password" => md5("password")
      ],
  ]
];

function getUserEmails($userName) {
    $emails = array_slice(scandir('./emails'), 2);

    $userEmails = [];

    foreach ($emails as $key => $info) {
        preg_match('/-'.$userName.'/', $info, $matches);
        if (count($matches) == 1) {
            array_push($userEmails, $info);
        }
    }

    return $userEmails;

}

function getCurrentUser() {
    if ($_SESSION['isLogined']) {
        return $_SESSION["user"]["login"];
    }
    return null;
}

if((!isset($_SESSION["isLogined"]) || !$_SESSION['isLogined']) && $_SERVER["REQUEST_URI"] != "/login.php") {
    header('Location: login.php');
}