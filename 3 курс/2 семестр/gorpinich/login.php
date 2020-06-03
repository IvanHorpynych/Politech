<?php
include_once "./config.php";


if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $login = $_POST['login'];
    $password = $_POST['password'];

    if ($login && $password) {
        foreach ($CONFIGS["userData"] as $userId => $data) {
            if ($data["login"] == $login && $data["password"] == md5($password)) {
                $_SESSION["isLogined"] = true;
                $_SESSION["user"] = $data;
            }
        }
    }
}

if(isset($_SESSION['isLogined'])) {
    header('Location: /');
} else {
    echo <<<HTML
<form action="/login.php" method="POST">
    <label for="login">Login</label>
    <input type="text" name="login">
    <br>
    <label for="password">Password</label>
    <input type="password" name="password">
    <br>
    <input type="submit" value="Login">
</form>
HTML;

}