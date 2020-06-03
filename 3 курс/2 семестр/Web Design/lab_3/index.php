<?php
include_once "./config.php";

echo "Hi, " . $_SESSION["user"]["login"];
echo "<br><a href='/logout.php'>Logout</a>";

$emails = getUserEmails($_SESSION["user"]["login"]);

?>

<div >
    <a id='writeEmail' href="#">Write email</a>
    <form id="emailForm" style="display: none;" onsubmit="return formHandler();" >
        <label for="receiver">
            Receiver:
            <input type="text" name="receiver">
        </label>
        <br>
        <label for="subject">
            Subject:
            <input type="text" name="subject">
        </label>
        <br>
        <label for="content">
            Content:
            <textarea type="text" name="content"></textarea>
        </label>
        <br>
        <input type="submit" value="Send">
    </form>
</div>

<div>
<?php
if(count($emails)) {
    echo "Your received emails:<br>";
    foreach($emails as $email) {
        echo "<a href='/read.php?file=$email'>$email</a>" . "<br>";
    }
} else {
    echo "You haven't emails";
}

?>
</div>

<script>
    var openButton = document.getElementById('writeEmail');
    var form = document.getElementById('emailForm');
    openButton.onclick = function(e) {
        e.preventDefault();
        form.style.display = 'block';
    };

    function formHandler() {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function() {
           if (this.readyState !== 4) return;

           alert("Email sended");
        }

        xhr.open('POST', '/write.php');
        xhr.send(new FormData(form));

        return false;
    }


</script>
