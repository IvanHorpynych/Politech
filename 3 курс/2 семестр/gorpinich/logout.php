<?php
include_once "./config.php";

if (isset($_SESSION['isLogined'])) {
    session_destroy();
    header('Location: login.php');
}
header('Location: /');

